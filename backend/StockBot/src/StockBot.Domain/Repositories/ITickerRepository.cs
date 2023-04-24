using StockBot.Domain.Entities;

namespace StockBot.Domain.Repositories;

public interface ITickerRepository
{
    Task<bool> AddAsync(Ticker ticker);

    Task<Ticker> GetAsync(string key);
}