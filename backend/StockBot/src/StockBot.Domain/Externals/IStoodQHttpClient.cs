using StockBot.Domain.Entities;

namespace StockBot.Domain.Externals;

public interface IStoodQHttpClient
{
    Task<IEnumerable<ChatMessage>> GetStockInformationAsync(string stockCode);
}