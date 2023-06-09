﻿using System.Text.Json.Serialization;

namespace ChatService.Application.DTOs;

public class MessageDto
{
    [JsonPropertyName("user_name")]
    public string UserName { get; set; }

    [JsonPropertyName("chat_room")]
    public string ChatRoom { get; set; }
    
    [JsonPropertyName("time_stamp")]
    public string TimeStamp { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
}