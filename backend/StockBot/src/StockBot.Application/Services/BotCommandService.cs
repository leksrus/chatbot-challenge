using StockBot.Application.DTOs;
using StockBot.Application.Services.Interfaces;
using StockBot.Application.Support.Helpers;
using StockBot.Domain.Entities;
using StockBot.Domain.Externals;
using StockBot.Domain.Repositories;

namespace StockBot.Application.Services;

public class BotCommandService : IBotCommandService
{
    private readonly ITickerRepository _tickerRepository;
    private readonly IMqBrokerClient _mqBrokerClient;
    private readonly IStoodQHttpClient _stoodQHttpClient;

    public BotCommandService(ITickerRepository tickerRepository, IMqBrokerClient mqBrokerClient, IStoodQHttpClient stoodQHttpClient)
    {
        _tickerRepository = tickerRepository;
        _mqBrokerClient = mqBrokerClient;
        _stoodQHttpClient = stoodQHttpClient;
    }

    public async Task ExecuteStockCommandAsync(ChatRequestDto chatRequestDto)
    {
        var bot = new Bot();
        var commandSplit = chatRequestDto.Command.Split("=");

        if (!bot.IsValidCommand(commandSplit[0])) throw new BusinessException("Command not Valid");

        var ticker = await _tickerRepository.GetAsync(commandSplit[1].ToUpper());

        if (ticker is null) throw new BusinessException("Wrong Symbol");

        var chatMessages = await _stoodQHttpClient.GetStockInformationAsync(ticker.Symbol);

        foreach (var chatMessage in chatMessages)
        {
            chatMessage.ChannelName = chatMessage.ChannelName;
            chatMessage.MessageTime = DateTime.Now;
            chatMessage.UserName = "Chat Bot";
           
            await _mqBrokerClient.SendMessage(chatMessage);
        }
    }
}