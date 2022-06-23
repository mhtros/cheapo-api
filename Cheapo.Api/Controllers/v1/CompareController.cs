using System.Security.Claims;
using Cheapo.Api.Classes;
using Cheapo.Api.Classes.Attributes;
using Cheapo.Api.Classes.Responses;
using Cheapo.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cheapo.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/compare")]
public class CompareController : ControllerBase
{
    private readonly IApplicationTransactionRepository _transactions;

    public CompareController(IApplicationTransactionRepository transactions)
    {
        _transactions = transactions;
    }

    /// <summary>Returns income, expense sums amounts based on the category of two given dates.</summary>
    /// <response code="200">Retrieves compare results.</response>
    /// <response code="400">Not a valid period.</response>
    /// <param name="period">Date period on of "weekly", "monthly", "quarterly", "yearly".</param>
    /// <param name="date1">First date for comparision.</param>
    /// <param name="date2">Second date for comparision.</param>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{period}/{date1:datetime}/{date2:datetime}")]
    public async Task<IActionResult> Compare(string period, DateTime date1, DateTime date2)
    {
        var periods = new Dictionary<string, Func<DateTime, DateTime, Task<List<CompareResponse>>>>
        {
            { "weekly", CompareWeeksAsync },
            { "monthly", CompareMonthsAsync },
            { "quarterly", CompareQuartersAsync },
            { "yearly", CompareYearsAsync }
        };

        var key = period.ToLower();

        if (!periods.ContainsKey(key))
            return BadRequest(new ErrorResponse(new[] { Errors.InvalidPeriods }));

        var results = await periods[key](date1, date2);

        return Ok(new DataResponse<List<CompareResponse>>(results));
    }

    private async Task<List<CompareResponse>> CompareWeeksAsync(DateTime date1, DateTime date2)
    {
        DateTime GetWeekStart(DateTime date)
        {
            var daysFromStart = date1.DayOfWeek - DayOfWeek.Sunday;
            return date.AddDays(-daysFromStart);
        }

        DateTime GetWeekEnd(DateTime date)
        {
            var daysFromEnd = DayOfWeek.Saturday - date.DayOfWeek;
            return date.AddDays(daysFromEnd);
        }

        var date1WeekStart = GetWeekStart(date1);
        var date1WeekEnd = GetWeekEnd(date1);

        var date2WeekStart = GetWeekStart(date2);
        var date2WeekEnd = GetWeekEnd(date2);

        return await GetCompareResultsAsync(date1WeekStart, date1WeekEnd, date2WeekStart, date2WeekEnd);
    }

    private async Task<List<CompareResponse>> CompareMonthsAsync(DateTime date1, DateTime date2)
    {
        DateTime GetMonthStart(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        DateTime GetMonthEnd(DateTime date)
        {
            return GetMonthStart(date).AddMonths(1).AddDays(-1);
        }

        var date1MonthStart = GetMonthStart(date1);
        var date1MonthEnd = GetMonthEnd(date1);

        var date2MonthStart = GetMonthStart(date2);
        var date2MonthEnd = GetMonthEnd(date2);

        return await GetCompareResultsAsync(date1MonthStart, date1MonthEnd, date2MonthStart, date2MonthEnd);
    }

    private async Task<List<CompareResponse>> CompareQuartersAsync(DateTime date1, DateTime date2)
    {
        DateTime GetQuarterStart(DateTime date)
        {
            var quarter = (date.Month - 1) / 3 + 1;
            return new DateTime(date.Year, (quarter - 1) * 3 + 1, 1);
        }

        DateTime GetQuarterEnd(DateTime date)
        {
            return GetQuarterStart(date).AddMonths(3).AddDays(-1);
        }

        var date1QuarterStart = GetQuarterStart(date1);
        var date1QuarterEnd = GetQuarterEnd(date1);

        var date2QuarterStart = GetQuarterStart(date2);
        var date2QuarterEnd = GetQuarterEnd(date2);

        return await GetCompareResultsAsync(date1QuarterStart, date1QuarterEnd, date2QuarterStart, date2QuarterEnd);
    }

    private async Task<List<CompareResponse>> CompareYearsAsync(DateTime date1, DateTime date2)
    {
        var date1YearStart = new DateTime(date1.Year, 01, 01);
        var date1YearEnd = new DateTime(date1.Year, 12, 31);

        var date2YearStart = new DateTime(date2.Year, 01, 01);
        var date2YearEnd = new DateTime(date2.Year, 12, 31);

        return await GetCompareResultsAsync(date1YearStart, date1YearEnd, date2YearStart, date2YearEnd);
    }

    private async Task<List<CompareResponse>> GetCompareResultsAsync(
        DateTime date1Start, DateTime date1End, DateTime date2Start, DateTime date2End)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var query1 = _transactions.GetBaseQuery(userId);
        query1 = _transactions.ApplyFilters(query1, null, null, null, null, date1Start, date1End, null, false);

        var query2 = _transactions.GetBaseQuery(userId);
        query2 = _transactions.ApplyFilters(query2, null, null, null, null, date2Start, date2End, null, false);

        // Date 1 transactions sums group by category name, isExpense
        var result1 = await query1
            .GroupBy(x => new { x.Category.Name, x.IsExpense })
            .Select(x => new
            {
                CategoryName = x.Key.Name,
                isExpense = x.Key.IsExpense,
                TotalAmount = x.Sum(y => y.Amount)
            }).ToListAsync();

        // Date 1 transactions sums group by category name, isExpense
        var result2 = await query2
            .GroupBy(x => new { x.Category.Name, x.IsExpense })
            .Select(x => new
            {
                CategoryName = x.Key.Name,
                isExpense = x.Key.IsExpense,
                TotalAmount = x.Sum(y => y.Amount)
            }).ToListAsync();

        var results = new List<CompareResponse>();

        // Find all unique categories from both result1 and result2
        var categories = result1.Select(x => x.CategoryName).Union(result2.Select(x => x.CategoryName));

        foreach (var category in categories)
        {
            // Find all items with the given category name (up to a maximum of two items income, expense)
            var r1List = result1.Where(x => x.CategoryName == category).ToList();
            var r2List = result2.Where(x => x.CategoryName == category).ToList();


            if (r1List.Count > 0)
                foreach (var r1 in r1List)
                    // Add to results only if there is not another item with the same category and IsExpense.
                    if (!results.Any(x => x.Category == r1.CategoryName && x.IsExpense == r1.isExpense))
                        results.Add(new CompareResponse
                        {
                            Category = category,
                            Date1TotalAmount = r1.TotalAmount,
                            Date2TotalAmount =
                                r2List?.Find(x => x.CategoryName == r1.CategoryName && x.isExpense == r1.isExpense)
                                    ?.TotalAmount ?? 0,
                            IsExpense = r1.isExpense
                        });

            if (r2List?.Count > 0)
                foreach (var r2 in r2List)
                    // Add to results only if there is not another item with the same category and IsExpense.
                    if (!results.Any(x => x.Category == r2.CategoryName && x.IsExpense == r2.isExpense))
                        results.Add(new CompareResponse
                        {
                            Category = category,
                            Date1TotalAmount = r2.TotalAmount,
                            Date2TotalAmount =
                                r1List?.Find(x => x.CategoryName == r2.CategoryName && x.isExpense == r2.isExpense)
                                    ?.TotalAmount ?? 0,
                            IsExpense = r2.isExpense
                        });
        }

        return results;
    }
}