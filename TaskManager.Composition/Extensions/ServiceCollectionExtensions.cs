using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infrastructure.Extensions;
using TaskManager.Persistence;
using TaskManager.Persistence.Extensions;

namespace TaskManager.Composition.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTaskManagerServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext & Persistence (Repositories)
            services
                .AddDbContext(configuration)
                .AddPersistence();

            //Identity
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //JWT Auth & Authorization
            services
                .AddJwtAuthentication(configuration)
                .AddAuthorization();

            //Infrastructure & Application
            services
                .AddInfrastructure(configuration)
                .AddApplication();

            return services;
        }

    }
}
