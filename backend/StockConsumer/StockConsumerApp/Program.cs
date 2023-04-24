// See https://aka.ms/new-console-template for more information

using System.Text;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{environment}.json", true, true)
    .AddEnvironmentVariables();

var configuration = builder.Build();


Console.WriteLine($"... Starting Stock Consumer Subscriber ... HOST: {configuration.GetValue<string>("RabbitMQSettings:HostName")}");

var factory = new ConnectionFactory
{
    HostName = configuration.GetValue<string>("RabbitMQSettings:HostName"),
    Port = configuration.GetValue<int>("RabbitMQSettings:Port"),
    UserName = configuration.GetValue<string>("RabbitMQSettings:User"),
    Password = configuration.GetValue<string>("RabbitMQSettings:Password"),
};

var chatHubUrl = configuration.GetValue<string>("ChatHubUrl");

Console.WriteLine($"... Starting Connection to Chat Hub ...");

var hubConnection = new HubConnectionBuilder()
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

Console.WriteLine($"... Connecting to Rabbit MQ ... ");

var connection = factory.CreateConnection();

Console.WriteLine($"... Connect to MQ Complete ...");

using var model = connection.CreateModel();

var consumer = new EventingBasicConsumer(model);

var queueName = configuration.GetValue<string>("RabbitMQSettings:QueueName");

Console.WriteLine($"... Binding Subscriber to {queueName} ...");

consumer.Received += async (_, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"... Stock message received: {message} ...");

    await hubConnection.InvokeAsync("SendMessageToGroup", message);

    Console.WriteLine($"... Stock message sent to chat Hub ...");
};

model.BasicConsume(queueName, true, consumer);

Console.WriteLine($"... Waiting messages ...");

Console.ReadKey();