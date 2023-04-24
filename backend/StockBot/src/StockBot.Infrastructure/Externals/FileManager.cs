using StockBot.Domain.Externals;

namespace StockBot.Infrastructure.Externals;

public class FileManager : IFileManager
{
    public string[] GetFileFromDisk(string fileRoute)
    {
        if (!Directory.Exists(fileRoute)) return Directory.GetFiles(fileRoute);

        throw new FileLoadException("Cannot load file. Directory not found");
    }
}