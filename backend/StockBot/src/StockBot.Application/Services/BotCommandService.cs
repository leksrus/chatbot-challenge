using StockBot.Application.Services.Interfaces;
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

    public async Task ExecuteStockCommandAsync(string command)
    {
        var bot = new Bot();
        var commandSplit = command.Split("=");

        if (!bot.IsValidCommand(commandSplit[0])) throw new Exception("");

        // await _tickerRepository.AddAsync(new Ticker { Symbol = commandSplit[1], Description = "asdasdsa" });
        
        var ticker = await _tickerRepository.GetAsync(commandSplit[1]);

        if (ticker is null) throw new Exception("");

        var chatMessages = await _stoodQHttpClient.GetStockInformationAsync(ticker.Symbol);

        foreach (var chatMessage in chatMessages)
        {

            chatMessage.MessageTime = DateTime.Now;
            chatMessage.UserName = "Chat Bot";
           
            await _mqBrokerClient.SendMessage(chatMessage);
        }
        
    }
}