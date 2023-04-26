using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Application.Services;
using ChatService.Application.Support.Helpers.Exceptions;
using ChatService.Domain.Entities;
using ChatService.Domain.HttpClients;
using ChatService.Domain.Repositories;
using Moq;
using Xunit;

namespace ChatService.UnitTests.Application.Services;

public class MessagesServiceTest
{
    [Theory]
    [InlineData("/stock=ABG.US", "General")]
    public async Task When_ProcessMessageAsync_ExpectSendCommandToBot(string text, string chatRoom)
    {
        var chatBotHttpClient = new Mock<IChatBotHttpClient>();
        var messagesRepository = new Mock<IMessagesRepository>();
        var mapperMock = new Mock<IMapper>();

        var chatRequestDto = new ChatMessageDto
        {
            Text = text,
            ChatRoom = chatRoom
        };

        chatBotHttpClient.Setup(r => r.SendCommand(It.IsAny<BotMessage>()))
            .ReturnsAsync(new KeyValuePair<bool, string>(true, "Test"));
        mapperMock.Setup(r => r.Map<Message>(It.IsAny<ChatMessageDto>()))
            .Returns(new Message { Text = "/test", ChatRoom = "test", TimeStamp = "01/01/01"});

        var messagesService =
            new MessagesService(messagesRepository.Object, mapperMock.Object, chatBotHttpClient.Object);

        await messagesService.ProcessMessageAsync(chatRequestDto);

        chatBotHttpClient.Verify(f => f.SendCommand(It.IsAny<BotMessage>()), Times.Once);
    }
    
    [Theory]
    [InlineData("/stock=ABG.US", "General")]
    public async Task When_ProcessMessageAsync_BotFailRequest_ExpectThrewException(string text, string chatRoom)
    {
        var chatBotHttpClient = new Mock<IChatBotHttpClient>();
        var messagesRepository = new Mock<IMessagesRepository>();
        var mapperMock = new Mock<IMapper>();

        var chatRequestDto = new ChatMessageDto
        {
            Text = text,
            ChatRoom = chatRoom
        };

        chatBotHttpClient.Setup(r => r.SendCommand(It.IsAny<BotMessage>()))
            .ReturnsAsync(new KeyValuePair<bool, string>(false, "Test"));
        mapperMock.Setup(r => r.Map<Message>(It.IsAny<ChatMessageDto>()))
            .Returns(new Message { Text = "/test", ChatRoom = "test", TimeStamp = "01/01/01", UserName = "test" });

        var messagesService =
            new MessagesService(messagesRepository.Object, mapperMock.Object, chatBotHttpClient.Object);

        await Assert.ThrowsAsync<BusinessException>(() => messagesService.ProcessMessageAsync(chatRequestDto));
    }
    
    [Theory]
    [InlineData("stock=ABG.US", "General")]
    public async Task When_ProcessMessageAsync_SimpleText_ExpectToSaveMessage(string text, string chatRoom)
    {
        var chatBotHttpClient = new Mock<IChatBotHttpClient>();
        var messagesRepository = new Mock<IMessagesRepository>();
        var mapperMock = new Mock<IMapper>();

        var chatRequestDto = new ChatMessageDto
        {
            Text = text,
            ChatRoom = chatRoom,
        };

        chatBotHttpClient.Setup(r => r.SendCommand(It.IsAny<BotMessage>()))
            .ReturnsAsync(new KeyValuePair<bool, string>(false, "Test"));
        mapperMock.Setup(r => r.Map<Message>(It.IsAny<ChatMessageDto>()))
            .Returns(new Message { Text = "test", ChatRoom = "test", TimeStamp = "01/01/01" });

        var messagesService =
            new MessagesService(messagesRepository.Object, mapperMock.Object, chatBotHttpClient.Object);

        await messagesService.ProcessMessageAsync(chatRequestDto);

        messagesRepository.Verify(f => f.AddAsync(It.IsAny<Message>()), Times.Once);
    }
}