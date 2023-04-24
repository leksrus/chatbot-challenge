using Serilog;
using StockBot.API.BackgroundServices;
using StockBot.Application;
using StockBot.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddHttpClient();

builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<TickerSetBackgroundService>();

builder.Host.UseSerilog((_, _, configuration) => {
    configuration.WriteTo.Console();
});


var app = builder.Build();

app.UseExceptionHandler("/error");

app
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapHealthChecks("/health");        
    });

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
