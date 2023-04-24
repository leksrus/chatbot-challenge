using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using StackExchange.Redis;
using StockBot.Domain.Externals;
using StockBot.Domain.Repositories;
using StockBot.Infrastructure.Externals;
using StockBot.Infrastructure.RabbitMqConfig;
using StockBot.Infrastructure.Repositories;

namespace StockBot.Infrastructure
{
    public static class DependenciesRegisterExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqSettings = configuration.GetSection("RabbitMQSettings");

            services.Configure<RabbitMqConfiguration>(rabbitMqSettings);

            services.AddSingleton<IMqBrokerClient>(sp =>
            {
                var option = sp.GetService<IOptions<RabbitMqConfiguration>>();
                var factory = new ConnectionFactory
                {
                    HostName = option.Value.HostName,
                    Port = 5672,
                    UserName = "user",
                    Password = "password"
                };

                return new MqBrokerClient(factory.CreateConnection(), option);
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(new ConfigurationOptions
                {
                    EndPoints =
                        { $"{configuration.GetValue<string>("RedisCache:Host")}: {configuration.GetValue<int>("RedisCache:Port")}" }
                }));

            services.AddScoped<ITickerRepository, TickerRepository>();
            services.AddScoped<IStoodQHttpClient, StoodQHttpClient>();
            
            return services;
        }
    }
}