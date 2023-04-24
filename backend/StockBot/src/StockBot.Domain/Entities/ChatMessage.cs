namespace StockBot.Domain.Entities;

public class ChatMessage
{
    public string UserName { get; set; }

    public string ChannelName { get; set; }

    public DateTime MessageTime { get; set; }

    public string Text { get; set; }
}