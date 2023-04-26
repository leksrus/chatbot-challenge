using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockConsumerSub.Entities;
using StockConsumerSub.RabbitMqConfig;

namespace StockConsumerSub.BackgroundServices;

public class StockSubscriberBackgroundService : BackgroundService
{
    private readonly ILogger<StockSubscriberBackgroundService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IModel _model;
    private readonly IConnection _connection;
    private readonly RabbitMqConfiguration _rabbitMqConfiguration;

    public StockSubscriberBackgroundService(ILogger<StockSubscriberBackgroundService> logger, IConfiguration configuration, 
        IOptions<RabbitMqConfiguration> options)
    {
        _logger = logger;
        _configuration = configuration;
        _rabbitMqConfiguration = options.Value;
        _connection = GetRabbitMqConnection();
        _logger.LogInformation($"... Connect to MQ Complete ...");
        _model = _connection.CreateModel();
        _logger.LogInformation($"... Connect to MQ Complete ...");
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"... Creating Queues ...");

        _model.QueueDeclare(_rabbitMqConfiguration.QueueName, durable: true, autoDelete: false, exclusive: false);
        _model.QueueBind(_rabbitMqConfiguration.QueueName, _rabbitMqConfiguration.ExchangeName, _rabbitMqConfiguration.RoutingKey);

        var consumer = new EventingBasicConsumer(_model);

        _logger.LogInformation("... Binding Subscriber to {QueueName} ...", _rabbitMqConfiguration.QueueName);

        var hubConnection = await GetSignalHub();

        consumer.Received += async (_, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation("... Stock message received: {Message} ...", message);

            await hubConnection.InvokeAsync("SendMessageToGroup", JsonSerializer.Deserialize<ChatMessage>(message), cancellationToken: stoppingToken);

            _logger.LogInformation("... Stock message sent to chat Hub ...");
        };

        _model.BasicConsume(_rabbitMqConfiguration.QueueName, true, consumer);

        _logger.LogInformation($"... Waiting messages ...");
    }


    private async Task<HubConnection> GetSignalHub()
    {
        _logger.LogInformation($"... Starting Connection to Chat Hub ...");
        var chatHubUrl = _configuration.GetValue<string>("ChatHubUrl");
        
        var hubConnection = new HubConnectionBuilder()
            .WithUrl(chatHubUrl)
            .Build();
        
        hubConnection.Closed += async (error) =>
        {
            _logger.LogError("... Error {ErrorMessage} ...", error?.Message);
            await Task.Delay(TimeSpan.FromSeconds(5));

            Console.WriteLine($"... Starting Connection to Chat Hub ...");
            await hubConnection.StartAsync();
        };
        
        await hubConnection.StartAsync();
        
        _logger.LogInformation($"... Connection to Chat Hub Started ...");

        return hubConnection;
    }

    private IConnection GetRabbitMqConnection()
    {
        _logger.LogInformation("... Starting Stock Consumer Subscriber ... HOST: {Value}", _rabbitMqConfiguration.HostName);
        
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqConfiguration.HostName,
            Port = _rabbitMqConfiguration.Port,
            UserName = _rabbitMqConfiguration.User,
            Password = _rabbitMqConfiguration.Password
        };
        
        _logger.LogInformation($"... Connecting to Rabbit MQ ... ");

        return factory.CreateConnection();
    }
}