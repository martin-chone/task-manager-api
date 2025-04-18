using System.Security.Claims;
using TaskManager.Application.Dtos.Auth;

namespace TaskManager.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        TokenResponse GenerateToken(IEnumerable<Claim> claims);
    }
}
