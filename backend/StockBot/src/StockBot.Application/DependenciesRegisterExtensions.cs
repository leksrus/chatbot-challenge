using Microsoft.Extensions.DependencyInjection;
using StockBot.Application.Services;
using StockBot.Application.Services.Interfaces;

namespace StockBot.Application
{
    public static class DependenciesRegisterExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBotCommandService, BotCommandService>();
            services.AddScoped<ITickerServices, TickerService>();

            return services;
        }
    }
}