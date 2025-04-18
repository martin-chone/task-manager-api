using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Factories.Claims;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Factories.Claims;
using TaskManager.Application.Services;
using TaskManager.Application.Services.Auth;

namespace TaskManager.Infrastructure.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Services 
            services.AddScoped<ITaskItemService, TaskItemService>();

            // Factories
            services.AddScoped<IClaimsFactory, IdentityClaimsFactory>();

            // Auth services
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
