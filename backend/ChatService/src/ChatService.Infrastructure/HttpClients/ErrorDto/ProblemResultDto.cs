using System.Text.Json.Serialization;

namespace ChatService.Infrastructure.HttpClients.ErrorDto;

public class ProblemResultDto
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }
    
    [JsonPropertyName("details")]
    public string Details { get; set; }
}