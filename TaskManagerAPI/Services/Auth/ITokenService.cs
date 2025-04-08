using System.Security.Claims;
using TaskManagerAPI.Dtos;

namespace TaskManagerAPI.Services.Auth
{
    public interface ITokenService
    {
        GeneratedTokenDto GenerateToken(IEnumerable<Claim> claims);
    }
}
