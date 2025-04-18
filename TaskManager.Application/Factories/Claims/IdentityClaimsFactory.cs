using System.Security.Claims;
using TaskManager.Application.Dtos.Claims;
using TaskManager.Application.Interfaces.Factories.Claims;

namespace TaskManager.Application.Factories.Claims
{
    public class IdentityClaimsFactory : IClaimsFactory
    {
        public IdentityClaimsFactory()
        {
        }

        public IEnumerable<Claim> GetClaimsForUser(ClaimsRequest request)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, request.Id),
                new Claim(ClaimTypes.Email, request.Email)
            };

            claims.AddRange(request.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}
