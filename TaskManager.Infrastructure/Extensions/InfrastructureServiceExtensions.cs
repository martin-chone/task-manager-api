using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Infrastructure.Services;
using TaskManager.Infrastructure.Services.Auth;

namespace TaskManager.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<IUserService, UserService>();

            // Auth services
            services.AddScoped<IIdentityService, IdentityService>();

            // Services techniques (Email, JWT, etc.)
            services.AddScoped<ITokenService, JwtTokenService>();

            return services;
        }
    }
}
