using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // using statement to create a scope that will dispose when done. 
            using (var scope = host.Services.CreateScope()) 
            {
                // Get services
                var services = scope.ServiceProvider;
                // Get logger factory from services - so we can create a logger for class and log errors
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try 
                {
                    // Get the db context
                    var context = services.GetRequiredService<StoreContext>();

                    // Apply any pending migrations for the context to the database and create the database if one does
                    // not exist
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAysnc(context, loggerFactory);

                } catch (Exception ex)
                {
                    // Create logger
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred during startup/migration");
                }

            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
