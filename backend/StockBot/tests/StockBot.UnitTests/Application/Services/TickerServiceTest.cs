using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using StockBot.Application.Services;
using StockBot.Domain.Entities;
using StockBot.Domain.Externals;
using StockBot.Domain.Repositories;
using Xunit;

namespace StockBot.UnitTests.Application.Services;

public class TickerServiceTest
{
    
    [Fact]
    public async Task When_LoadTicketAsync_ExpectToAddAllTickets()
    {
        var fileManagerMock = new Mock<IFileManager>();
        var tickerRepositoryMock = new Mock<ITickerRepository>();
        fileManagerMock.Setup(r => r.GetTickersFromFileAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(GetTickers());

        var tickerService = new TickerService(tickerRepositoryMock.Object, fileManagerMock.Object);

       await tickerService.LoadTickersAsync();
       
       fileManagerMock.Verify(f => f.GetTickersFromFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
       
       tickerRepositoryMock.Verify(t => t.AddAsync(It.IsAny<Ticker>()), Times.Once);
    }
    
    [Fact]
    public async Task When_LoadTicketAsyncWithEmptyFile_ExpectToAddAllTickets()
    {
        var fileManagerMock = new Mock<IFileManager>();
        var tickerRepositoryMock = new Mock<ITickerRepository>();
        fileManagerMock.Setup(r => r.GetTickersFromFileAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new List<Ticker>());

        var tickerService = new TickerService(tickerRepositoryMock.Object, fileManagerMock.Object);

        await tickerService.LoadTickersAsync();
       
        fileManagerMock.Verify(f => f.GetTickersFromFileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
       
        tickerRepositoryMock.Verify(t => t.AddAsync(It.IsAny<Ticker>()), Times.Never);
    }


    private IEnumerable<Ticker> GetTickers()
    {
        return new List<Ticker>
        {
            new()
            {
                Name = "Apple Ticker",
                Symbol = "APPL.US"
            }
        };
    }
}