using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using StockBot.Application.DTOs;
using StockBot.Application.Services;
using StockBot.Application.Support.Helpers;
using StockBot.Domain.Entities;
using StockBot.Domain.Externals;
using StockBot.Domain.Repositories;
using Xunit;

namespace StockBot.UnitTests.Application.Services;

public class BotCommandServiceTest
{
    [Theory]
    [InlineData("/stock=ABG.US", "General")]
    public async Task When_ExecuteStockCommandAsync_ExpectToPublishInformationToRabbitMQ(string command, string chatRoom)
    {
        var brokerClientMock = new Mock<IMqBrokerClient>();
        var tickerRepositoryMock = new Mock<ITickerRepository>();
        var stoodQHttpClientMock = new Mock<IStoodQHttpClient>();

        var chatRequestDto = new ChatRequestDto
        {
            Command = command,
            ChatRoom = chatRoom
        };
        
        stoodQHttpClientMock.Setup(r => r.GetStockInformationAsync(It.IsAny<string>()))
            .ReturnsAsync(GetChatMessages());
        
        tickerRepositoryMock.Setup(r => r.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new Ticker {Name = "Apple Ticker", Symbol = "APPL.US"});

        var botCommandServiceService = new BotCommandService(tickerRepositoryMock.Object, brokerClientMock.Object, stoodQHttpClientMock.Object);

        await botCommandServiceService.ExecuteStockCommandAsync(chatRequestDto);
       
        tickerRepositoryMock.Verify(f => f.GetAsync(It.IsAny<string>()), Times.Once);
       
        stoodQHttpClientMock.Verify(t => t.GetStockInformationAsync(It.IsAny<string>()), Times.Once);
        
        brokerClientMock.Verify(t => t.SendMessage(It.IsAny<ChatMessage>()), Times.Once);
    }
    
    [Theory]
    [InlineData("stock=ABG.US", "General")]
    public async Task When_ExecuteStockCommandAsync_NotValidCommand_ExpectThrowException(string command, string chatRoom)
    {
        var brokerClientMock = new Mock<IMqBrokerClient>();
        var tickerRepositoryMock = new Mock<ITickerRepository>();
        var stoodQHttpClientMock = new Mock<IStoodQHttpClient>();

        var chatRequestDto = new ChatRequestDto
        {
            Command = command,
            ChatRoom = chatRoom
        };

        var botCommandServiceService = new BotCommandService(tickerRepositoryMock.Object, brokerClientMock.Object, stoodQHttpClientMock.Object);

        await Assert.ThrowsAsync<BusinessException>( () =>botCommandServiceService.ExecuteStockCommandAsync(chatRequestDto));
    }

    [Theory]
    [InlineData("stockABG.US", "General")]
    public async Task When_ExecuteStockCommandAsync_WithNoEqualValue_ExpectThrowException(string command, string chatRoom)
    {
        var brokerClientMock = new Mock<IMqBrokerClient>();
        var tickerRepositoryMock = new Mock<ITickerRepository>();
        var stoodQHttpClientMock = new Mock<IStoodQHttpClient>();

        var chatRequestDto = new ChatRequestDto
        {
            Command = command,
            ChatRoom = chatRoom
        };

        var botCommandServiceService = new BotCommandService(tickerRepositoryMock.Object, brokerClientMock.Object, stoodQHttpClientMock.Object);

        await Assert.ThrowsAsync<BusinessException>( () =>botCommandServiceService.ExecuteStockCommandAsync(chatRequestDto));
    }
    
    [Theory]
    [InlineData("stockABGGGG.US", "General")]
    public async Task When_ExecuteStockCommandAsync_WrongSymbol_ExpectThrowException(string command, string chatRoom)
    {
        var brokerClientMock = new Mock<IMqBrokerClient>();
        var tickerRepositoryMock = new Mock<ITickerRepository>();
        var stoodQHttpClientMock = new Mock<IStoodQHttpClient>();

        var chatRequestDto = new ChatRequestDto
        {
            Command = command,
            ChatRoom = chatRoom
        };

        var botCommandServiceService = new BotCommandService(tickerRepositoryMock.Object, brokerClientMock.Object, stoodQHttpClientMock.Object);

        await Assert.ThrowsAsync<BusinessException>( () =>botCommandServiceService.ExecuteStockCommandAsync(chatRequestDto));
    }

    private IEnumerable<ChatMessage> GetChatMessages()
    {
        return new List<ChatMessage>
        {
            new ChatMessage
            {
                Text = "test",
                ChatRoom = "Test",
                TimeStamp = "01/01/01",
                UserName = "Test"
            }
        };
    }
}