namespace Cheapo.Api.Extensions;

/// <summary>
///     Helper Method to extract properties out of the <see cref="HttpContext" /> object.
/// </summary>
public static class HttpContextExtractor
{
    /// <summary>
    ///     Extract the body out of the context request.
    /// </summary>
    /// <param name="context"><see cref="HttpContext" />.</param>
    /// <returns>The body string.</returns>
    public static async Task<string> ExtractBodyAsync(this HttpContext context)
    {
        var stream = context.Request.Body;
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(stream);
        var payload = await reader.ReadToEndAsync();
        return payload;
    }

    /// <summary>
    ///     Extract the headers out of the context request.
    /// </summary>
    /// <param name="context"><see cref="HttpContext" />.</param>
    /// <returns>The header string.</returns>
    public static string ExtractHeaders(this HttpContext context)
    {
        return "{" + string.Join(",", context.Request.Headers.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
    }
}