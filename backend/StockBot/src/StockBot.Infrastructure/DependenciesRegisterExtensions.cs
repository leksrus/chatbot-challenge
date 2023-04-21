


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StockBot.Infrastructure
{
    public static class DependenciesRegisterExtensions
    {
        /// <summary>
        /// Aqui se deben setear las dependencias
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            

            return services;
        }
    }
}