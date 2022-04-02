using System.Text;
using Cheapo.Api.Interfaces.Services;

namespace Cheapo.Api.Classes.Services;

public class SaveToFile : ISaveToFile
{
    /// <summary>
    ///     Creates a file asynchronously. If file already exist then
    ///     appends the content at the end of the file.
    /// </summary>
    public async Task SaveToFileAsync(string path, string filename, string content)
    {
        try
        {
            Directory.CreateDirectory(path);

            var filePath = Path.Combine(path, filename);
            var encodedText = Encoding.Unicode.GetBytes(content);

            const FileMode mode = FileMode.Append;
            const FileAccess access = FileAccess.Write;
            const FileShare share = FileShare.None;

            await using var sourceStream = new FileStream(filePath, mode, access, share, 4096, true);
            await sourceStream.WriteAsync(encodedText.AsMemory(0, encodedText.Length));
        }
        catch (IOException ioException)
        {
            throw new IOException(ioException.Message);
        }
    }
}