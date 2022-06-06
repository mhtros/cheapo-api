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
[Route("api/v{version:apiVersion}/transaction-categories")]
public class TransactionCategoriesController : ControllerBase
{
    private readonly IApplicationTransactionCategoriesRepository _transactionCategories;

    public TransactionCategoriesController(IApplicationTransactionCategoriesRepository transactionCategories)
    {
        _transactionCategories = transactionCategories;
    }

    /// <summary>Retrieves category transactions records.</summary>
    /// <param name="pagingParams">
    ///     <see cref="PaginationModel" />
    /// </param>
    /// <param name="name">filter by name.</param>
    /// <param name="userOnly">
    ///     If true then it will return only the categories that the specific user has create.
    ///     Default value: FALSE
    /// </param>
    /// <response code="200">Retrieves all the category transactions records.</response>
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

    /// <summary>Create a new transaction category.</summary>
    /// <response code="409">Transaction category name already exists.</response>
    /// <response code="422">Record was not saved.</response>
    /// <response code="201">Successfully create the new transaction category.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<IActionResult> CreateTransactionCategory(TransactionCategoryModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var exists = await _transactionCategories.ExistsAsync(model);
        if (exists) return Conflict(new ErrorResponse(new[] { Errors.AlreadyExists }));

        var entity = new ApplicationTransactionCategory
        {
            Id = Guid.NewGuid().ToString(),
            Name = model.Name,
            UserId = userId
        };

        await _transactionCategories.AddAsync(entity);

        var saved = await _transactionCategories.SaveAsync();
        if (!saved) return UnprocessableEntity(new ErrorResponse(new[] { Errors.EntityNotSaved }));

        return Created(nameof(CreateTransactionCategory), new DataResponse<TransactionCategoriesResponse>(
            new TransactionCategoriesResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                UserId = entity.UserId
            }));
    }

    /// <summary>Deletes a transaction category.</summary>
    /// <response code="404">Transaction category not exists.</response>
    /// <response code="422">Record was not removed.</response>
    /// <response code="204">Successfully remove transaction category.</response>
    [JwtAuthentication]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransactionCategory(string id)
    {
        var exists = await _transactionCategories.ExistsAsync(id);
        if (!exists) return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var category = await _transactionCategories.FindById(id);

        if (category == null || !category.BelongTo(userId)) return NotFound();

        //TODO: update all transaction records that have this category to uncategorized. 

        _transactionCategories.Remove(category);
        var saved = await _transactionCategories.SaveAsync();
        if (!saved) return UnprocessableEntity(new ErrorResponse(new[] { Errors.EntityNotRemoved }));

        return NoContent();
    }

    // HELPING METHODS

    private void AddPagination<T>(PagedList<T>? list)
    {
        if (list == null) return;
        Response.AddPaginationHeader(list.CurrentPage, list.PageSize, list.TotalCount, list.TotalPages);
    }
}