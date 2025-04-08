using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TaskManagerAPI.Factories.Claims
{
    public interface IClaimFactory
    {
        Task<IEnumerable<Claim>> GetClaimsForUserAsync(IdentityUser user);
    }
}
