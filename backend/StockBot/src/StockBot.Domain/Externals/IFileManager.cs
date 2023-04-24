using StockBot.Domain.Entities;

namespace StockBot.Domain.Externals;

public interface IFileManager
{
    Task<IEnumerable<Ticker>> GetTickersFromFileAsync(string fileRoute, string fileName);
}