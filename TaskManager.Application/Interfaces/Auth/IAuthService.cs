using TaskManager.Application.Dtos.Auth;
using TaskManager.Shared;

namespace TaskManager.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterRequest request, string roleName);
        Task<Result<AuthResponse>> RegisterAndLoginAsync(RegisterRequest request, string roleName);
        Task<Result<AuthResponse>> LoginAsync(LoginRequest request);
    }
}
