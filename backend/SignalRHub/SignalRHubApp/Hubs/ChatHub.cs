using Microsoft.AspNetCore.SignalR;
using SignalRHubApp.Entities;
using SignalRHubApp.Interfaces;

namespace SignalRHubApp.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessageToGroup(ChatMessage chatMessage)
        => await Clients.Group(chatMessage.ChannelName).ReceiveMessage(chatMessage);
}