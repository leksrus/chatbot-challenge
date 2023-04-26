namespace StockBot.Application.Services.Interfaces;

public interface ITickerServices
{
    Task LoadTickersAsync();
}