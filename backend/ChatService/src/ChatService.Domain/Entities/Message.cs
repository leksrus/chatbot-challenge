namespace ChatService.Domain.Entities;

public class Message
{
    public string UserName { get; set; }

    public string ChatRoom { get; set; }

    public string TimeStamp { get; set; }

    public string Text { get; set; }
}