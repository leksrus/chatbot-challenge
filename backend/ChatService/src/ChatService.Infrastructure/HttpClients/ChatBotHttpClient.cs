using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using AutoMapper;
using ChatService.Application.DTOs;
using ChatService.Domain.Entities;
using ChatService.Domain.HttpClients;
using ChatService.Infrastructure.HttpClients.ErrorDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChatService.Infrastructure.HttpClients;

public class ChatBotHttpClient : IChatBotHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public ChatBotHttpClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMapper mapper)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _mapper = mapper;
    }
    
    public async Task<KeyValuePair<bool, string>> SendCommand(BotMessage botMessage)
    {
        var uri = new Uri(_configuration.GetValue<string>("ChatBotUrl"));
        
        var chatMessageDto = _mapper.Map<ChatBotDto>(botMessage);

        var json = JsonSerializer.Serialize(chatMessageDto);
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var httpClient = _httpClientFactory.CreateClient();

        var response = await httpClient.PostAsync(uri, content);
        
        if(response.StatusCode == HttpStatusCode.Accepted) 
            return new KeyValuePair<bool, string>(true, "Command Accepted");

        var data = await response.Content.ReadAsStringAsync();

        var errorResult = JsonSerializer.Deserialize<ProblemResultDto>(data);

        return new KeyValuePair<bool, string>(false, errorResult.Title);
    }
}