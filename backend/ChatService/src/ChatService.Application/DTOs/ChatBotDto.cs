using System.Text.Json.Serialization;

namespace ChatService.Application.DTOs;

public class ChatBotDto
{
    [JsonPropertyName("command")]
    public string Command { get; set; }

    [JsonPropertyName("chat_room")]
    public string ChatRoom { get; set; }
}