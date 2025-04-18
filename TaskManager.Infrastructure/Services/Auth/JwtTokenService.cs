using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Application.Dtos.Auth;
using TaskManager.Application.Interfaces.Auth;
using TaskManager.Infrastructure.Settings;

namespace TaskManager.Infrastructure.Services.Auth
{
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public TokenResponse GenerateToken(IEnumerable<Claim> claims)
        {
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentials
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return new TokenResponse
            {
                Token = handler.WriteToken(token),
                ExpiresAt = token.ValidTo
            };
        }
    }
}
