using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Persistence.Repositories;

namespace TaskManager.Persistence.Extensions
{
    public static class PersistenceServiceExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IRepository<TaskItem>, TaskItemRepository>();

            return services;
        }
    }
}
