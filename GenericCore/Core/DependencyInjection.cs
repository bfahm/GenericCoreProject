using GenericCore.Core.Mappers;
using GenericCore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GenericCore.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            
            //services.AddScoped<MapperDependency>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new AccountsProfile());
                cfg.ConstructServicesUsing(type => ActivatorUtilities.CreateInstance(serviceProvider, type));
            });

            return services;
        }

        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            // Automapper
            services.AddAutomapper();

            // Core Services
            services.AddScoped<AccountService>();

            return services;
        }
    }
}
