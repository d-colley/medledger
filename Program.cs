using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MedLedger.Data;
using MedLedger.Areas.Identity.Data;

namespace MedLedger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    MedLedgerDBContext context = services.GetRequiredService<MedLedgerDBContext>();
                    MedLedgerMVCIdentityDbContext identity_context = services.GetRequiredService<MedLedgerMVCIdentityDbContext>();
                    //var identity_context = services.GetRequiredService<MedLedgerMVCIdentityDbContext>();
                    //DbInitializer.Initialize(context);
                    //DbInitializer.Initialize(identity_context);

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
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
