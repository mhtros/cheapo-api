using System.Security.Claims;
using Cheapo.Api.Classes;
using Cheapo.Api.Classes.Attributes;
using Cheapo.Api.Classes.Models;
using Cheapo.Api.Classes.Pagination;
using Cheapo.Api.Classes.Responses;
using Cheapo.Api.Extensions;
using Cheapo.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cheapo.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/transaction-categories")]
public class TransactionCategoriesController : ControllerBase
{
    private readonly IApplicationTransactionCategoriesRepository _transactionCategories;

    public TransactionCategoriesController(IApplicationTransactionCategoriesRepository transactionCategories)
    {
        _transactionCategories = transactionCategories;
    }

    /// <summary>
    ///     Retrieves category transactions records.
    /// </summary>
    /// <param name="pagingParams">
    ///     <see cref="PaginationModel" />
    /// </param>
    /// <param name="name">filter by name.</param>
    /// <param name="userOnly">
    ///     If true then it will return only the categories that the specific user has create.
    ///     Default value: FALSE
    /// </param>
    /// <response code="200">
    ///     Retrieves all the category transactions records.
    /// </response>
    /// <response code="400">If query parameters are invalid.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetTransactionCategories([FromQuery] PaginationModel pagingParams,
        [FromQuery] string? name, [FromQuery] bool userOnly = false)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var baseQuery = _transactionCategories.GetBaseQuery(userId, userOnly);

        IQueryable<TransactionCategoriesResponse> filterQuery = null!;

        var hasFilters = !string.IsNullOrWhiteSpace(name);
        if (hasFilters) filterQuery = _transactionCategories.ApplyFilters(baseQuery, name!);

        if (pagingParams.PageNumber == 0 || pagingParams.PageSize == 0)
            return BadRequest(new ErrorResponse(new[] { Errors.InvalidQueryParameters }));

        var query = hasFilters ? filterQuery : baseQuery;
        var paginateCategories = await _transactionCategories.GetRecordsAsync(query, pagingParams);

        AddPagination(paginateCategories);
        return Ok(new Response<List<TransactionCategoriesResponse>>(paginateCategories));
    }

    // HELPING METHODS

    private void AddPagination<T>(PagedList<T>? list)
    {
        if (list == null) return;
        Response.AddPaginationHeader(list.CurrentPage, list.PageSize, list.TotalCount, list.TotalPages);
    }
}