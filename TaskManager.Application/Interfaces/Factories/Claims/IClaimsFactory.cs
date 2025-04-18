using System.Security.Claims;
using TaskManager.Application.Dtos.Claims;

namespace TaskManager.Application.Interfaces.Factories.Claims
{
    public interface IClaimsFactory
    {
        IEnumerable<Claim> GetClaimsForUser(ClaimsRequest request);
    }
}
