using System.Text.Json.Serialization;

namespace StockBot.Application.DTOs;

public class ChatRequestDto
{
    [JsonPropertyName("command")]
    public string Command { get; set; }

    [JsonPropertyName("chat_room")]
    public string ChatRoom { get; set; }
}