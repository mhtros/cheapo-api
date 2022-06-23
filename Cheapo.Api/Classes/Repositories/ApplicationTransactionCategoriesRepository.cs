using Cheapo.Api.Classes.Models;
using Cheapo.Api.Classes.Pagination;
using Cheapo.Api.Classes.Responses;
using Cheapo.Api.Data;
using Cheapo.Api.Entities;
using Cheapo.Api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cheapo.Api.Classes.Repositories;

public class ApplicationTransactionCategoriesRepository : BaseRepository, IApplicationTransactionCategoriesRepository
{
    public ApplicationTransactionCategoriesRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<TransactionCategoriesResponse> GetBaseQuery(string userId, bool userOnly = false)
    {
        var dbSet = Context.ApplicationTransactionCategories;

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
        string? name)
    {
        if (name != null)
            query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));

        return query;
    }

    public async Task<PagedList<TransactionCategoriesResponse>> GetRecordsAsync(
        IQueryable<TransactionCategoriesResponse> query, PaginationModel pagingParams)
    {
        return await PagedList<TransactionCategoriesResponse>.CreateAsync(query, pagingParams.PageNumber,
            pagingParams.PageSize);
    }

    public async Task AddAsync(ApplicationTransactionCategory entity)
    {
        await Context.ApplicationTransactionCategories.AddAsync(entity);
    }

    public async Task<bool> ExistsAsync(TransactionCategoryModel model)
    {
        return await Context.ApplicationTransactionCategories.AnyAsync(x => x.Name == model.Name);
    }

    public async Task<bool> ExistsAsync(string userId, TransactionCategoryModel model)
    {
        return await Context.ApplicationTransactionCategories.AnyAsync(x =>
            x.Name == model.Name && (x.UserId == userId || x.User == null));
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await Context.ApplicationTransactionCategories.AnyAsync(x => x.Id == id);
    }

    public async Task<ApplicationTransactionCategory?> FindById(string id)
    {
        return await Context.ApplicationTransactionCategories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Remove(ApplicationTransactionCategory entity)
    {
        Context.ApplicationTransactionCategories.Remove(entity);
    }
}