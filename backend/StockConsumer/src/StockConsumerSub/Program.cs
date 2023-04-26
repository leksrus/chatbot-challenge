using Serilog;
using StockConsumerSub.BackgroundServices;
using StockConsumerSub.RabbitMqConfig;

var builder = WebApplication.CreateBuilder(args);

var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQSettings");
builder.Services.Configure<RabbitMqConfiguration>(rabbitMqSettings);
builder.Services.AddHostedService<StockSubscriberBackgroundService>();


builder.Host.UseSerilog((_, _, configuration) => {
    configuration.WriteTo.Console();
});


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();