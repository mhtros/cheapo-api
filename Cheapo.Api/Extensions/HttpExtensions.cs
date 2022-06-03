using System.Text.Json;
using Cheapo.Api.Classes.Pagination;

namespace Cheapo.Api.Extensions;

public static class HttpExtensions
{
    /// <summary>
    ///     Add pagination information in HTTP response header.
    /// </summary>
    public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage,
        int totalItems, int totalPages)
    {
        var paginationHeaders = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeaders, options));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}