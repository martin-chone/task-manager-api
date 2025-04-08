using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using TaskManagerAPI.Data;
using TaskManagerAPI.Settings;

namespace TaskManagerAPI.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, config) =>
                config.ReadFrom.Configuration(context.Configuration));
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection service, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            service.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return service;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection service)
        {
            service.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return service;
        }

        public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection service, IConfiguration config)
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

            service.AddAuthorization();

            return service;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection service)
        {
            service.AddEndpointsApiExplorer();

            service.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Veuillez entrer votre token avec ce format : 'Bearer <token>'",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });

            return service;
        }
    }
}
