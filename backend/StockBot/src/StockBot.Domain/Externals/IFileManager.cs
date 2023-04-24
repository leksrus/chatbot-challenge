namespace StockBot.Domain.Externals;

public interface IFileManager
{
    string[] GetFileFromDisk(string fileRoute);
}