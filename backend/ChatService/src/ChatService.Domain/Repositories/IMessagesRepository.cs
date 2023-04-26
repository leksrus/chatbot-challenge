using ChatService.Domain.Entities;

namespace ChatService.Domain.Repositories;

public interface IMessagesRepository
{
    Task<IEnumerable<Message>> GetLastMessagesAsync(string chatRoom, int messagesCount);

    Task<Message> AddAsync(Message message);
}