namespace StockBot.Application.Services.Interfaces;

public interface IBotCommandService
{
    Task ExecuteStockCommandAsync(string command);
}