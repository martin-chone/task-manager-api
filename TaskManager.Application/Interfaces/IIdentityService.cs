using TaskManager.Application.Dtos.Auth;
using TaskManager.Shared;

namespace TaskManager.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<Result> RegisterAsync(RegisterRequest request, string roleName);
        Task<Result<AuthResponse>> RegisterAndLoginAsync(RegisterRequest request, string roleName);
        Task<Result<AuthResponse>> LoginAsync(LoginRequest request);
    }
}
