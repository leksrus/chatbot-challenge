namespace StockBot.API.BackgroundServices;

public class TickerSetBackgroundService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        
        throw new NotImplementedException();
    }
}