using Microsoft.EntityFrameworkCore;

namespace Cheapo.Api.Classes.Pagination;

public class PagedList<T> : List<T>
{
    private PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        TotalCount = count;
        AddRange(items);
    }

    public int CurrentPage { get; }

    public int TotalPages { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    /// <summary>
    ///     Creates a pagination list asynchronously.
    /// </summary>
    /// <param name="sourceQuery"><see cref="IQueryable{T}" />.</param>
    /// <param name="pageNumber">Retrieving page.</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <returns>
    ///     Pagination information along with the <see cref="List{T}" />.
    /// </returns>
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> sourceQuery, int pageNumber, int pageSize)
    {
        var count = await sourceQuery.CountAsync();

        var items = await sourceQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}