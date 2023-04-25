using ChatService.Domain.Entities;

namespace ChatService.Domain.HttpClients;

public interface IChatBotHttpClient
{
    Task<KeyValuePair<bool, string>> SendCommand(BotMessage botMessage);
}