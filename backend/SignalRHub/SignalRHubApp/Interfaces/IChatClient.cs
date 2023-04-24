using SignalRHubApp.Entities;

namespace SignalRHubApp.Interfaces;

public interface IChatClient
{
    Task ReceiveMessage(ChatMessage message);
}