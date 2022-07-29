using GenericCore.Models;
using GenericCore.Persistence.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GenericCore.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, AppSettings appSettings)
        {
            // Setup the connextion string to be used by AppDbContext
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(appSettings.SQLServerConnectionString);
            });

            // Setup ASP Identity to use the database
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
