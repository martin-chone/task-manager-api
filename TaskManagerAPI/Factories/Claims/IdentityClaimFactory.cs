using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TaskManagerAPI.Factories.Claims
{
    public class IdentityClaimFactory : IClaimFactory
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityClaimFactory(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<Claim>> GetClaimsForUserAsync(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}
