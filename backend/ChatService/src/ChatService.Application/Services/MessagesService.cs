using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Application.Services.Interfaces;
using ChatService.Domain.Entities;
using ChatService.Domain.Repositories;

namespace ChatService.Application.Services;

public class MessagesService : IMessagesService
{
    private const int TopMessages = 50;

    private readonly IMessagesRepository _messagesRepository;
    private readonly IMapper _mapper;

    public MessagesService(IMessagesRepository messagesRepository, IMapper mapper)
    {
        _messagesRepository = messagesRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MessageDto>> GetLastMessagesForChannelAsync(string chatRoom)
    {
        var messages = await _messagesRepository.GetLastMessagesAsync(chatRoom, TopMessages);
        
        return _mapper.Map<IEnumerable<MessageDto>>(messages);
    }

    public async Task<MessageDto> AddMessageAsync(MessageDto messageDto)
    {
        var message = _mapper.Map<Message>(messageDto);

        var newMessage = await _messagesRepository.Add(message);

        return _mapper.Map<MessageDto>(newMessage);
    }
}