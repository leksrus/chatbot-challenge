using StockBot.Application.Services.Interfaces;

namespace StockBot.API.BackgroundServices;

public class TickerSetBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public TickerSetBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var tickerService = scope.ServiceProvider.GetRequiredService<ITickerServices>();

        await tickerService.LoadTickersAsync();
    }
}