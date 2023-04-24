using StockBot.Application.DTOs;

namespace StockBot.Application.Services.Interfaces;

public interface IBotCommandService
{
    Task ExecuteStockCommandAsync(ChatRequestDto chatRequestDto);
}