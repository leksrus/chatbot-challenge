using ChatService.Domain.Auth;
using ChatService.Domain.Crypto;
using ChatService.Domain.Repositories;
using ChatService.Infrastructure.Auth;
using ChatService.Infrastructure.Crypto;
using ChatService.Infrastructure.Repositories;
using ChatService.Infrastructure.Support.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ChatService.Infrastructure
{
    public static class DependenciesRegisterExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            services.Configure<JwtSettings>(jwtSettings);
            
            services.AddScoped<ICryptoManager, CryptoManager>();
            services.AddScoped<IJwtManager, JwtManager>();
            services.AddScoped<IMessagesRepository, MessagesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            services.AddSingleton<IMongoDatabase>(_ => {
                var settings =  configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
                var client = new MongoClient(settings.ConnectionString);
    
                return client.GetDatabase(settings.DatabaseName);
            });

            services.AddAutoMapper(cfg => { cfg.AddMaps(AutoMapperConfig.RegisterMappings()); });

            return services;
        }
    }
}