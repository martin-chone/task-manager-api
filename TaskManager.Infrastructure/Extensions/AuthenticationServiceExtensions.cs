using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManager.Infrastructure.Settings;

namespace TaskManager.Infrastructure.Extensions
{
    public static class AuthenticationServiceExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection service, IConfiguration config)
        {
            var jwtSettings = config.GetSection("Jwt").Get<JwtSettings>();

            if (jwtSettings == null ||
                string.IsNullOrEmpty(jwtSettings.Secret) ||
                string.IsNullOrEmpty(jwtSettings.Issuer) ||
                string.IsNullOrEmpty(jwtSettings.Audience))
            {
                throw new ArgumentNullException("JwtSettings", "JWT configuration is incomplete.");
            }

            byte[] key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            return service;
        }
    }
}
