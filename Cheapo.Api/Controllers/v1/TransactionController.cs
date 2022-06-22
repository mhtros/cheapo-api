using System.Security.Claims;
using Cheapo.Api.Classes;
using Cheapo.Api.Classes.Attributes;
using Cheapo.Api.Classes.Models;
using Cheapo.Api.Classes.Pagination;
using Cheapo.Api.Classes.Responses;
using Cheapo.Api.Entities;
using Cheapo.Api.Extensions;
using Cheapo.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cheapo.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/transactions")]
public class TransactionController : ControllerBase
{
    private readonly IApplicationTransactionCategoriesRepository _transactionCategories;
    private readonly IApplicationTransactionRepository _transactions;

    public TransactionController(IApplicationTransactionRepository transactions,
        IApplicationTransactionCategoriesRepository transactionCategories)
    {
        _transactions = transactions;
        _transactionCategories = transactionCategories;
    }

    /// <summary>Retrieves transaction records.</summary>
    /// <param name="pagingParams">
    ///     <see cref="PaginationModel" />
    /// </param>
    /// <param name="description">What description will it contain.</param>
    /// <param name="categoryId">Category to which it will belong.</param>
    /// <param name="amountFrom">Minimum amount.</param>
    /// <param name="amountTo">Maximum amount.</param>
    /// <param name="createdFrom">Shorter date.</param>
    /// <param name="createdTo">Longer date.</param>
    /// <param name="isExpense">If it's income or expense.</param>
    /// <param name="ignoreDays">Does not take days into account when it comes to date comparision.</param>
    /// <response code="200">Retrieves all the transaction records.</response>
    /// <response code="400">If query parameters are invalid.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetTransactions([FromQuery] PaginationModel pagingParams,
        [FromQuery] string? description, [FromQuery] string? categoryId, [FromQuery] decimal? amountFrom,
        [FromQuery] decimal? amountTo, [FromQuery] DateTime? createdFrom, [FromQuery] DateTime? createdTo,
        [FromQuery] bool? isExpense, [FromQuery] bool ignoreDays)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var query = _transactions.GetBaseQuery(userId);
        query = _transactions.ApplyFilters(query, description, categoryId, amountFrom, amountTo, createdFrom,
            createdTo, isExpense, ignoreDays);

        if (pagingParams.PageNumber == 0 || pagingParams.PageSize == 0)
            return BadRequest(new ErrorResponse(new[] { Errors.InvalidQueryParameters }));

        var transactions = await _transactions.GetRecordsAsync(query, pagingParams);

        AddPagination(transactions);
        return Ok(new DataResponse<List<TransactionResponse>>(transactions));
    }

    /// <summary>Retrieves single transaction.</summary>
    /// <response code="200">Retrieves single transaction.</response>
    /// <response code="404">Transaction not found.</response>
    /// <param name="id">Transaction identifier.</param>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingleTransaction(string id)
    {
        var exists = await _transactions.ExistsAsync(id);
        if (!exists) return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var entity = await _transactions.FindById(id);

        if (entity == null) return NotFound();
        if (!entity.BelongTo(userId)) return NotFound();

        var transaction = new TransactionResponse
        {
            Id = entity.Id,
            Description = entity.Description,
            Amount = entity.Amount,
            Category = new TransactionCategoriesResponse
            {
                Id = entity.Category!.Id,
                Name = entity.Category.Name,
                UserId = entity.Category.UserId
            },
            Comments = entity.Comments,
            IsExpense = entity.IsExpense,
            UserId = entity.UserId,
            TransactionDate = entity.TransactionDate.ToDateTime(new TimeOnly())
        };

        return Ok(new DataResponse<TransactionResponse>(transaction));
    }

    /// <summary>Create a new transaction.</summary>
    /// <response code="422">Transaction was not saved.</response>
    /// <response code="400">Invalid payload.</response>
    /// <response code="201">Successfully create the new transaction.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<IActionResult> CreateTransaction(TransactionModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var categoryExists = await _transactionCategories.ExistsAsync(model.CategoryId);
        if (!categoryExists) return BadRequest();

        var transactionDate = new DateOnly(
            model.TransactionDate.Year,
            model.TransactionDate.Month,
            model.TransactionDate.Day);

        var entity = new ApplicationTransaction
        {
            Id = Guid.NewGuid().ToString(),
            TransactionDate = transactionDate,
            Description = model.Description,
            Amount = model.Amount,
            CategoryId = model.CategoryId,
            Category = await _transactionCategories.FindById(model.CategoryId),
            Comments = model.Comments,
            IsExpense = model.IsExpense,
            UserId = userId
        };

        await _transactions.AddAsync(entity);
        var saved = await _transactions.SaveAsync();

        if (!saved) return UnprocessableEntity(new ErrorResponse(new[] { Errors.EntityNotSaved }));

        return Created(nameof(CreateTransaction), new DataResponse<TransactionResponse>(
            new TransactionResponse
            {
                Id = entity.Id,
                Description = entity.Description,
                Amount = entity.Amount,
                Category = new TransactionCategoriesResponse
                {
                    Id = entity.Category!.Id,
                    Name = entity.Category.Name,
                    UserId = entity.Category.UserId
                },
                Comments = entity.Comments,
                IsExpense = entity.IsExpense,
                UserId = entity.UserId,
                TransactionDate = entity.TransactionDate.ToDateTime(new TimeOnly())
            }));
    }

    /// <summary>Updates a transaction.</summary>
    /// <response code="404">Transaction not found.</response>
    /// <response code="400">Invalid payload.</response>
    /// <response code="422">Transaction was not saved.</response>
    /// <response code="200">Successfully update the transaction.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaction(string id, TransactionUpdateModel model)
    {
        var exists = await _transactions.ExistsAsync(id);
        if (!exists) return NotFound();

        var categoryExists = await _transactionCategories.ExistsAsync(model.CategoryId);
        if (!categoryExists) return BadRequest();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var entity = await _transactions.FindById(id);

        if (entity == null) return NotFound();
        if (!entity.BelongTo(userId)) return NotFound();

        entity.Description = model.Description;
        entity.Amount = model.Amount;
        entity.CategoryId = model.CategoryId;
        entity.Category = await _transactionCategories.FindById(entity.CategoryId);
        entity.Comments = model.Comments;

        var saved = await _transactions.SaveAsync();
        if (!saved) return UnprocessableEntity(new ErrorResponse(new[] { Errors.EntityNotUpdated }));

        var transaction = new TransactionResponse
        {
            Id = entity.Id,
            Description = entity.Description,
            Amount = entity.Amount,
            Category = new TransactionCategoriesResponse
            {
                Id = entity.Category!.Id,
                Name = entity.Category.Name,
                UserId = entity.Category.UserId
            },
            Comments = entity.Comments,
            IsExpense = entity.IsExpense,
            UserId = entity.UserId,
            TransactionDate = entity.TransactionDate.ToDateTime(new TimeOnly())
        };

        return Ok(new DataResponse<TransactionResponse>(transaction));
    }

    /// <summary>Deletes a transaction.</summary>
    /// <response code="404">Transaction not found.</response>
    /// <response code="422">Transaction was not removed.</response>
    /// <response code="204">Successfully remove transaction.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(string id)
    {
        var exists = await _transactions.ExistsAsync(id);
        if (!exists) return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var transaction = await _transactions.FindById(id);

        if (transaction == null || !transaction.BelongTo(userId)) return NotFound();


        _transactions.Remove(transaction);
        var saved = await _transactions.SaveAsync();
        if (!saved) return UnprocessableEntity(new ErrorResponse(new[] { Errors.EntityNotRemoved }));

        return NoContent();
    }

    /// <summary>Get user total amount of money available.</summary>
    /// <response code="401">If the user credentials is wrong.</response>
    /// <response code="200">Balance.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("balance")]
    public async Task<IActionResult> GetBalance()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var balance = await _transactions.GetBalanceAsync(userId);
        return Ok(new DataResponse<decimal>(balance));
    }

    // HELPING METHODS

    private void AddPagination<T>(PagedList<T>? list)
    {
        if (list == null) return;
        Response.AddPaginationHeader(list.CurrentPage, list.PageSize, list.TotalCount, list.TotalPages);
    }
}