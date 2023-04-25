using ChatService.Domain.Entities;

namespace ChatService.Domain.HttpClients;

public interface IChatBotHttpClient
{
    Task SendCommand(Bot bot);
}