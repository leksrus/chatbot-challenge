using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using StockBot.Domain.Externals;
using StockBot.Infrastructure.RabbitMqConfig;

namespace StockBot.Infrastructure.Externals;

public class MqBrokerClient : IMqBrokerClient
{
    private readonly IModel _model;

    private readonly RabbitMqConfiguration _rabbitMqConfiguration;
    
    public MqBrokerClient(IConnection connection, IOptions<RabbitMqConfiguration> options)
    {
        _model = connection.CreateModel();
        _rabbitMqConfiguration = options.Value;
    }
    
    public Task SendMessage<T>(T message)
    {
        
        var messageJson = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(messageJson);

        _model.BasicPublish(exchange: _rabbitMqConfiguration.ExchangeName,
            routingKey: _rabbitMqConfiguration.RoutingKey,
            basicProperties: null,
            body: body);

        return Task.CompletedTask;
    }
}