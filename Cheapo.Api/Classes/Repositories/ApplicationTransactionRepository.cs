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
            .Include(x => x.Category)
            .Where(t => t.UserId == userId)
            .Select(x => new TransactionResponse
            {
                Amount = x.Amount,
                Comments = x.Comments,
                Description = x.Description,
                Id = x.Id,
                TransactionDate = x.TransactionDate.ToDateTime(new TimeOnly()),
                Category = new TransactionCategoriesResponse
                {
                    Id = x.Category!.Id,
                    Name = x.Category.Name,
                    UserId = x.Category.UserId
                },
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
        return await Context.ApplicationTransactions
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Remove(ApplicationTransaction entity)
    {
        Context.ApplicationTransactions.Remove(entity);
    }

    public IQueryable<TransactionResponse> ApplyFilters(IQueryable<TransactionResponse> query, string? description,
        string? categoryId, decimal? amountFrom, decimal? amountTo, DateTime? createdFrom, DateTime? createdTo,
        bool? isExpense, bool ignoreDays)
    {
        if (description != null)
            query = query.Where(x => x.Description.ToLower().Contains(description.ToLower()));

        if (categoryId != null)
            query = query.Where(x => x.Category.Id == categoryId);

        // only amount from was given
        if (amountFrom != null && amountTo == null)
            query = query.Where(x => x.Amount >= amountFrom);

        // only amount to was given
        if (amountTo != null && amountFrom == null)
            query = query.Where(x => x.Amount <= amountTo);

        // both amounts (from, to) was given
        if (amountTo != null && amountFrom != null)
            query = query.Where(x => x.Amount >= amountFrom && x.Amount <= amountTo);

        // only date from was given
        if (createdFrom != null && createdTo == null)
            query = query.Where(x => x.TransactionDate >= createdFrom.Value.Date);

        // only date to was given
        if (createdTo != null && createdFrom == null)
            query = query.Where(x => x.TransactionDate <= createdTo.Value.Date);

        // both dates (from, to) was given
        if (createdFrom != null && createdTo != null)
            query = query.Where(x =>
                (ignoreDays
                    ? x.TransactionDate.Year >= createdFrom.Value.Year &&
                      x.TransactionDate.Month >= createdFrom.Value.Month
                    : x.TransactionDate.Year >= createdFrom.Value.Year &&
                      x.TransactionDate.Month >= createdFrom.Value.Month &&
                      x.TransactionDate.Day >= createdFrom.Value.Day)
                && (ignoreDays
                    ? x.TransactionDate.Year <= createdTo.Value.Year && x.TransactionDate.Month <= createdTo.Value.Month
                    : x.TransactionDate.Year <= createdTo.Value.Year &&
                      x.TransactionDate.Month <= createdTo.Value.Month &&
                      x.TransactionDate.Day <= createdTo.Value.Day));

        if (isExpense != null)
            query = query.Where(x => x.IsExpense == isExpense);

        return query;
    }

    public async Task<decimal> GetBalanceAsync(string userId)
    {
        return await Context.ApplicationTransactions
            .Where(x => x.UserId == userId)
            .SumAsync(x => x.IsExpense ? -x.Amount : x.Amount);
    }
}