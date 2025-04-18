using AutoMapper;
using TaskManager.Application.Dtos.Auth;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Application.Interfaces.Factories.Claims;
using TaskManager.Shared;

namespace TaskManager.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IIdentityService identityService;

        public AuthService(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public Task<Result> RegisterAsync(RegisterRequest request, string roleName)
            => identityService.RegisterAsync(request, roleName);

        public Task<Result<AuthResponse>> RegisterAndLoginAsync(RegisterRequest request, string roleName)
            => identityService.RegisterAndLoginAsync(request, roleName);

        public Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
            => identityService.LoginAsync(request);
    }
}
