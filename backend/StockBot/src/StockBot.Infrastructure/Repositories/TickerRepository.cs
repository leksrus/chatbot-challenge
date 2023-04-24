using StackExchange.Redis;
using StockBot.Domain.Entities;
using StockBot.Domain.Repositories;

namespace StockBot.Infrastructure.Repositories;

public class TickerRepository : ITickerRepository
{
    private readonly IDatabaseAsync _database;
    
    public TickerRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }
    
    public async Task<bool> AddAsync(Ticker ticker)
    {
        return await _database.StringSetAsync(ticker.Symbol, ticker.Description);
    }

    public async Task<Ticker> GetAsync(string key)
    {
        var value = await _database.StringGetAsync(key);

        return value.HasValue ? new Ticker {Symbol = key, Description = value} : null;
    }
    
}