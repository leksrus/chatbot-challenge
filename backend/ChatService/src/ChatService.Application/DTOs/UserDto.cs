using System.Text.Json.Serialization;

namespace ChatService.Application.DTOs;

public class UserDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Password { get; set; }

    [JsonPropertyName("repeat_password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string RepeatPassword { get; set; }
}