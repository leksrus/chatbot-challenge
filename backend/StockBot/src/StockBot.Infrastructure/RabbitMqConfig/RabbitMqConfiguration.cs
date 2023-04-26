namespace StockBot.Infrastructure.RabbitMqConfig;

public class RabbitMqConfiguration
{
    public string HostName { get; set; }

    public int Port { get; set; }

    public string User { get; set; }
    
    public string Password { get; set; }

    public string ExchangeName { get; set; }

    public string ExchangeType { get; set; }

    public string RoutingKey { get; set; }
    
    public string QueueName { get; set; }
}