using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.DAL.Data;
using Talabat.DAL.Entities.Identity;
using Talabat.DAL.Identity;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();

                await StoreContextSeed.SeedAsync(context , LoggerFactory);

                var identitycontext = services.GetRequiredService<AppIdentityDbContext>();
                await identitycontext.Database.MigrateAsync();

                var UserManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(UserManager);


            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, ex.Message);
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
