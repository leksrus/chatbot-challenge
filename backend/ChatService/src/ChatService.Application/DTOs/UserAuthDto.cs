using System.Text.Json.Serialization;

namespace ChatService.Application.DTOs;

public class UserAuthDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Password { get; set; }
}