using Cheapo.Api.Interfaces.Services;
using ImageMagick;

namespace Cheapo.Api.Classes.Services;

public class ThumbnailGenerator : IThumbnailGenerator
{
    /// <summary>
    ///     Generates a thumbnail from a base64 string.
    /// </summary>
    /// <param name="imageBase64">Image base64 string.</param>
    /// <returns>
    ///     The thumbnails base64 string.
    /// </returns>
    public string Generate(string? imageBase64)
    {
        if (imageBase64 == null) return string.Empty;
        
        var imgBase64 = imageBase64.Replace("data:image/jpeg;base64,", "");
        using var image = MagickImage.FromBase64(imgBase64);

        image.Resize(32, 32);
        image.Strip();
        image.Quality = 75;

        return "data:image/jpeg;base64," + image.ToBase64();
    }
}