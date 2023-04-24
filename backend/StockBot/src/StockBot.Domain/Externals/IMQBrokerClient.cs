namespace StockBot.Domain.Externals;

public interface IMqBrokerClient
{
    Task SendMessage<T>(T message);
}