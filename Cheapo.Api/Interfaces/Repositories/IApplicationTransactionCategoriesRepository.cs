using Cheapo.Api.Classes.Models;
using Cheapo.Api.Classes.Pagination;
using Cheapo.Api.Classes.Responses;
using Cheapo.Api.Entities;

namespace Cheapo.Api.Interfaces.Repositories;

public interface IApplicationTransactionCategoriesRepository : ISaveable
{
    /// <summary>
    ///     Generates query for transaction category records retrieval.
    /// </summary>
    /// <param name="userId">Current user identifier.</param>
    /// <param name="userOnly">If true it will return only the records made by current the user.</param>
    public IQueryable<TransactionCategoriesResponse> GetBaseQuery(string userId, bool userOnly = false);

    /// <summary>
    ///     Adds additional filters to the search query.
    /// </summary>
    /// <param name="query">Query to which the filters will be applied.</param>
    /// <param name="name">Name filter.</param>
    public IQueryable<TransactionCategoriesResponse> ApplyFilters(IQueryable<TransactionCategoriesResponse> query,
        string? name);

    /// <summary>
    ///     Execute the <see cref="IQueryable{T}" /> query and return the result with pagination.
    /// </summary>
    /// <param name="query">Query for execution.</param>
    /// <param name="pagingParams"><see cref="PaginationModel" />.</param>
    public Task<PagedList<TransactionCategoriesResponse>> GetRecordsAsync(
        IQueryable<TransactionCategoriesResponse> query, PaginationModel pagingParams);

    /// <summary>
    ///     Adds a new item into the database table.
    /// </summary>
    public Task AddAsync(ApplicationTransactionCategory entity);

    /// <summary>
    ///     Checks if a item already exists on the database table.
    /// </summary>
    public Task<bool> ExistsAsync(TransactionCategoryModel model);

    /// <summary>
    ///     Checks if a item already exists on the database table for a specific user.
    /// </summary>
    public Task<bool> ExistsAsync(string userId, TransactionCategoryModel model);

    /// <summary>
    ///     Checks if a item already exists on the database table.
    /// </summary>
    public Task<bool> ExistsAsync(string id);

    /// <summary>
    ///     Finds the item with the specific id. If not exists return Null.
    /// </summary>
    public Task<ApplicationTransactionCategory?> FindById(string id);

    /// <summary>
    ///     Removes an item from the database table.
    /// </summary>
    public void Remove(ApplicationTransactionCategory entity);

    /// <summary>
    ///     Removes all transaction of a specific user.
    /// </summary>
    public void UserRemoveAll(string userId);
}