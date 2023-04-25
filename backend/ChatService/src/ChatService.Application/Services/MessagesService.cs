using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Application.Services.Interfaces;
using ChatService.Application.Support.Helpers.Exceptions;
using ChatService.Domain.Entities;
using ChatService.Domain.HttpClients;
using ChatService.Domain.Repositories;

namespace ChatService.Application.Services;

public class MessagesService : IMessagesService
{
    private const int TopMessages = 50;

    private readonly IMessagesRepository _messagesRepository;
    private readonly IMapper _mapper;
    private readonly IChatBotHttpClient _chatBotHttpClient;

    public MessagesService(IMessagesRepository messagesRepository, IMapper mapper, IChatBotHttpClient chatBotHttpClient)
    {
        _messagesRepository = messagesRepository;
        _mapper = mapper;
        _chatBotHttpClient = chatBotHttpClient;
    }

    public async Task<IEnumerable<MessageDto>> GetLastMessagesForChannelAsync(string chatRoom)
    {
        var messages = await _messagesRepository.GetLastMessagesAsync(chatRoom, TopMessages);
        
        return _mapper.Map<IEnumerable<MessageDto>>(messages);
    }

    public async Task<MessageDto> ProcessMessageAsync(ChatMessageDto chatMessageDto)
    {
        var message = _mapper.Map<Message>(chatMessageDto);

        var firstChar = message.Text.Substring(0, 1);

        if (!firstChar.Equals("/"))
        {
            var newMessage = await _messagesRepository.Add(message);
        
            return _mapper.Map<MessageDto>(newMessage);
        }

        var botMessage = new BotMessage
        {
            ChatRoom = message.ChatRoom,
            Command = message.Text
        };

        var (key, value) = await _chatBotHttpClient.SendCommand(botMessage);

        if (!key) throw new BusinessException(value);
        
        return _mapper.Map<MessageDto>(message);
    }
}