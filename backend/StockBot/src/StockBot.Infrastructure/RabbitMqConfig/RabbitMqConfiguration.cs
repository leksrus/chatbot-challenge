namespace StockBot.Infrastructure.RabbitMqConfig;

public class RabbitMqConfiguration
{
    public string HostName { get; set; }

    public string ExchangeName { get; set; }

    public string ExchangeType { get; set; }

    public string RoutingKey { get; set; }
}