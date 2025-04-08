using TaskManagerAPI.Common;
using TaskManagerAPI.Dtos;

namespace TaskManagerAPI.Services.Auth
{
    public interface IAuthService
    {
        Task<Result<string>> RegisterAsync(RegisterUserDto model, string roleName);
        Task<Result<AuthResultDto>> LoginAsync(LoginUserDto model);
    }
}
