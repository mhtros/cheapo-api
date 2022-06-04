using Cheapo.Api.Classes.Models;
using Cheapo.Api.Classes.Pagination;
using Cheapo.Api.Classes.Responses;
using Cheapo.Api.Data;
using Cheapo.Api.Interfaces.Repositories;

namespace Cheapo.Api.Classes.Repositories;

public class ApplicationTransactionCategoriesRepository : IApplicationTransactionCategoriesRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationTransactionCategoriesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<TransactionCategoriesResponse> GetBaseQuery(string userId, bool userOnly = false)
    {
        var dbSet = _context.ApplicationTransactionCategories;

        var baseQuery = userOnly
            ? dbSet.Where(tc => tc.UserId == userId)
            : dbSet.Where(tc => tc.UserId == null || tc.UserId == userId);

        return baseQuery.Select(x => new TransactionCategoriesResponse
        {
            Id = x.Id,
            Name = x.Name,
            UserId = x.UserId
        });
    }

    public IQueryable<TransactionCategoriesResponse> ApplyFilters(IQueryable<TransactionCategoriesResponse> query,
        string name)
    {
        return query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
    }

    public async Task<PagedList<TransactionCategoriesResponse>> GetRecordsAsync(
        IQueryable<TransactionCategoriesResponse> query, PaginationModel pagingParams)
    {
        return await PagedList<TransactionCategoriesResponse>.CreateAsync(query, pagingParams.PageNumber,
            pagingParams.PageSize);
    }
}