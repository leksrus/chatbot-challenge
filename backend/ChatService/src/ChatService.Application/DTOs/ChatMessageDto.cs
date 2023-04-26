using System.Text.Json.Serialization;

namespace ChatService.Application.DTOs;

public class ChatMessageDto
{
    [JsonIgnore]
    public string? UserName { get; set; }

    [JsonPropertyName("chat_room")]
    public string ChatRoom { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
}