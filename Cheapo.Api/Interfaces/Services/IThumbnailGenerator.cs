namespace Cheapo.Api.Interfaces.Services;

public interface IThumbnailGenerator
{
    /// <summary>
    ///     Generates a thumbnail from a base64 string.
    /// </summary>
    /// <param name="imageBase64">Image base64 string.</param>
    /// <returns>
    ///     The thumbnails base64 string.
    /// </returns>
    public string Generate(string? imageBase64);
}