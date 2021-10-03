using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericCore.Helpers;
using GenericCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Serilog;

namespace GenericCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();

                var sp = host.Services.CreateScope().ServiceProvider;
                var appSettings = sp.GetRequiredService<IOptions<AppSettings>>().Value;

                Log.Logger = SerilogInstaller.CreateLogger(appSettings);

                Log.Information("Starting host...");

                var environment = sp.GetRequiredService<IWebHostEnvironment>();
                if (!environment.IsProduction())
                {
                    IdentityModelEventSource.ShowPII = true;
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .UseSerilog()
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>();
                       });
        }
    }
}
