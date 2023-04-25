using ChatService.Application.DTOs;

namespace ChatService.Application.Services.Interfaces;

public interface IMessagesService
{
    Task<IEnumerable<MessageDto>> GetLastMessagesForChannelAsync(string chatRoom);

    Task<MessageDto> ProcessMessageAsync(ChatMessageDto chatMessageDto);
}