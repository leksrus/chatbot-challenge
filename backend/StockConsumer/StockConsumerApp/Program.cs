// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockConsumerApp.Entities;

const string queueName = "stock_queue";
const string chatHubUrl = "http://chatsignalrhub/chatHub";

Console.WriteLine($"... Starting Stock Consumer Subscriber ...");

var factory = new ConnectionFactory {
    HostName = "localhost",
    Port = 5672,
    UserName = "test_user",
    Password = "test_password"
};


Console.WriteLine($"... Starting Connection to Chat Hub ...");

var hubConnection  = new HubConnectionBuilder()
    .WithUrl(chatHubUrl)
    .Build();

hubConnection.Closed += async (error) =>
{
    Console.WriteLine($"... Error {error?.Message} ...");
    await Task.Delay(5000);
    
    Console.WriteLine($"... Starting Connection to Chat Hub ...");
    await hubConnection.StartAsync();
};

await hubConnection.StartAsync();

Console.WriteLine($"... Connection to Chat Hub Started ...");

Console.WriteLine($"... Connecting to Rabbit MQ ...");

var connection = factory.CreateConnection();

Console.WriteLine($"... Connect to MQ Complete ...");

using var model = connection.CreateModel();

var consumer = new EventingBasicConsumer(model);

Console.WriteLine($"... Binding Subscriber to {queueName} ...");

consumer.Received += async (_, eventArgs) => {
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"... Stock message received: {message} ...");
    
    var chatMessage = JsonSerializer.Deserialize<ChatMessage>(message);
    
    await hubConnection.InvokeAsync("SendMessageToGroup", chatMessage);
    
    Console.WriteLine($"... Stock message sent to chat Hub ...");
};

model.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

Console.WriteLine($"... Waiting messages ...");

Console.ReadKey();