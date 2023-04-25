using System.Text.Json.Serialization;

namespace ChatService.Application.DTOs;

public class AuthDto
{
    [JsonPropertyName("token")]
    public string Token { get; set; }
}