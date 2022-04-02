namespace Cheapo.Api.Interfaces.Services;

public interface ISaveToFile
{
    /// <summary>
    ///     Creates a file asynchronously. If file already exist then
    ///     appends the content at the end of the file.
    /// </summary>
    public Task SaveToFileAsync(string path, string filename, string content);
}