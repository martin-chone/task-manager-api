using TaskManagerAPI.Factories.Claims;
using TaskManagerAPI.Services;
using TaskManagerAPI.Services.Auth;

namespace TaskManagerAPI.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection service)
        {
            service.AddScoped<IAuthService, AuthService>();
            service.AddSingleton<ITokenService, JwtTokenService>();
            service.AddScoped<IUserService, UserService>();

            service.AddScoped<ITaskItemService, TaskItemService>();

            service.AddScoped<IClaimFactory, IdentityClaimFactory>();

            return service;
        }
    }
}
