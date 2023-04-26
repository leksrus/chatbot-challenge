using System.Globalization;
using AutoMapper;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using StockBot.Application.DTOs;
using StockBot.Domain.Entities;
using StockBot.Domain.Externals;

namespace StockBot.Infrastructure.Externals;

public class StoodQHttpClient : IStoodQHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public StoodQHttpClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMapper mapper)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ChatMessage>> GetStockInformationAsync(string stockCode)
    {
        var uri = _configuration.GetValue<string>("StoodQUrl");

        using var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync(string.Format(uri, stockCode));

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(string.Concat("Error on Get Request with status code:  ", response.StatusCode));
        
        using var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
        using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            
        var csvList = csv.GetRecords<StoodQMessageDto>();

        return _mapper.Map<IEnumerable<ChatMessage>>(csvList);
    }
}