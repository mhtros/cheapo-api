using Cheapo.Api.Classes.Models;
using Cheapo.Api.Classes.Pagination;
using Cheapo.Api.Classes.Responses;
using Cheapo.Api.Data;
using Cheapo.Api.Entities;
using Cheapo.Api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cheapo.Api.Classes.Repositories;

public class ApplicationTransactionRepository : BaseRepository, IApplicationTransactionRepository
{
    public ApplicationTransactionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<TransactionResponse> GetBaseQuery(string userId)
    {
        var dbSet = Context.ApplicationTransactions;

        return dbSet
            .Where(t => t.UserId == userId)
            .Select(x => new TransactionResponse
            {
                Amount = x.Amount,
                Comments = x.Comments,
                Description = x.Description,
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                CategoryId = x.CategoryId,
                IsExpense = x.IsExpense,
                UserId = x.UserId
            });
    }

    public async Task<PagedList<TransactionResponse>> GetRecordsAsync(IQueryable<TransactionResponse> query,
        PaginationModel pagingParams)
    {
        return await PagedList<TransactionResponse>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
    }

    public async Task AddAsync(ApplicationTransaction entity)
    {
        await Context.ApplicationTransactions.AddAsync(entity);
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await Context.ApplicationTransactions.AnyAsync(x => x.Id == id);
    }

    public async Task<ApplicationTransaction?> FindById(string id)
    {
        return await Context.ApplicationTransactions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Remove(ApplicationTransaction entity)
    {
        Context.ApplicationTransactions.Remove(entity);
    }

    public IQueryable<TransactionResponse> ApplyFilters(IQueryable<TransactionResponse> query, string? description,
        string? categoryId, decimal? amountFrom, decimal? amountTo, DateTime? createdFrom, DateTime? createdTo,
        bool? isExpense)
    {
        if (description != null)
            query = query.Where(x => x.Description.ToLower().Contains(description.ToLower()));

        if (categoryId != null)
            query = query.Where(x => x.CategoryId == categoryId);

        if (amountFrom != null)
            query = query.Where(x => x.Amount >= amountFrom);

        if (amountTo != null)
            query = query.Where(x => x.Amount <= amountTo);

        if (createdFrom != null)
            query = query.Where(x => x.CreatedAt >= createdFrom);

        if (createdTo != null)
            query = query.Where(x => x.CreatedAt <= createdTo);

        if (isExpense != null)
            query = query.Where(x => x.IsExpense == isExpense);

        return query;
    }
}