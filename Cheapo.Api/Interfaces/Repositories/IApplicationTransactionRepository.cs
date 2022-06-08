using Cheapo.Api.Classes.Models;
using Cheapo.Api.Classes.Pagination;
using Cheapo.Api.Classes.Responses;
using Cheapo.Api.Entities;

namespace Cheapo.Api.Interfaces.Repositories;

public interface IApplicationTransactionRepository : ISaveable
{
    /// <summary>
    ///     Generates query for transaction records retrieval.
    /// </summary>
    /// <param name="userId">Current user identifier.</param>
    public IQueryable<TransactionResponse> GetBaseQuery(string userId);

    /// <summary>Adds additional filters to the search query.</summary>
    /// <param name="query">Query to which the filters will be applied.</param>
    /// <param name="description">Description filter.</param>
    /// <param name="categoryId">Category filter.</param>
    /// <param name="amountFrom">Min amount filter.</param>
    /// <param name="amountTo">Max amount filter.</param>
    /// <param name="createdFrom">Min created date filter.</param>
    /// <param name="createdTo">Max created date filter.</param>
    /// <param name="isExpense">Income or expense filter.</param>
    /// <param name="ignoreDays">Ignore days on date times comparisons filter.</param>
    public IQueryable<TransactionResponse> ApplyFilters(IQueryable<TransactionResponse> query, string? description,
        string? categoryId, decimal? amountFrom, decimal? amountTo, DateTime? createdFrom, DateTime? createdTo,
        bool? isExpense, bool ignoreDays);

    /// <summary>
    ///     Execute the <see cref="IQueryable{T}" /> query and return the result with pagination.
    /// </summary>
    /// <param name="query">Query for execution.</param>
    /// <param name="pagingParams"><see cref="PaginationModel" />.</param>
    public Task<PagedList<TransactionResponse>> GetRecordsAsync(IQueryable<TransactionResponse> query,
        PaginationModel pagingParams);

    /// <summary>
    ///     Adds a new item into the database table.
    /// </summary>
    public Task AddAsync(ApplicationTransaction entity);

    /// <summary>
    ///     Checks if a item already exists on the database table.
    /// </summary>
    public Task<bool> ExistsAsync(string id);

    /// <summary>
    ///     Finds the item with the specific id. If not exists return Null.
    /// </summary>
    public Task<ApplicationTransaction?> FindById(string id);

    /// <summary>
    ///     Removes an item from the database table.
    /// </summary>
    public void Remove(ApplicationTransaction entity);

    /// <summary>
    ///     Total amount of money available.
    /// </summary>
    public Task<decimal> GetBalanceAsync(string userId);
}