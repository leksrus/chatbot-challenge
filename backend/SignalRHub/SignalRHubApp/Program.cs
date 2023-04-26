using Serilog;
using SignalRHubApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Host.UseSerilog((_, _, configuration) => {
    configuration.WriteTo.Console();
});

var app = builder.Build();

app.MapHub<ChatHub>("/chatHub");

app.Run();