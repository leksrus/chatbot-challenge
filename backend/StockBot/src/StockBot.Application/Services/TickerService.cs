using StockBot.Application.Services.Interfaces;
using StockBot.Domain.Externals;
using StockBot.Domain.Repositories;

namespace StockBot.Application.Services;

public class TickerService : ITickerServices
{
    private readonly ITickerRepository _tickerRepository;
    private readonly IFileManager _fileManager;

    public TickerService(ITickerRepository tickerRepository, IFileManager fileManager)
    {
        _tickerRepository = tickerRepository;
        _fileManager = fileManager;
    }

    public async Task LoadTickersAsync()
    {
        var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var fileDirectory = string.Concat(appDirectory, "Tickers");

        var tickers = await _fileManager.GetTickersFromFileAsync(fileDirectory, "Tickers.csv");

        foreach (var ticker in tickers)
        {
            ticker.Symbol = string.Concat(ticker.Symbol, ".US");
            await _tickerRepository.AddAsync(ticker);
        }
    }
}