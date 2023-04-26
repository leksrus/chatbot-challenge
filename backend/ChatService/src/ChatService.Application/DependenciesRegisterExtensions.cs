using ChatService.Application.Services;
using ChatService.Application.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChatService.Application
{
    public static class DependenciesRegisterExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsersServices, UsersService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMessagesService, MessagesService>();
            
            
            services.AddAutoMapper(cfg => { cfg.AddMaps(AutoMapperConfig.RegisterMappings()); });
            
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblies(FluentValidationConfig.RegisterValidations());

            return services;
        }
    }
}