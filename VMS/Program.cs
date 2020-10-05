using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Microsoft.Extensions.DependencyInjection;
using Lingkail.VMS.Data;
using Lingkail.VMS.Auth.Web.Data;
using IdentityServer4.EntityFramework.DbContexts;

namespace Lingkail.VMS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            /*
             * Note: 
             *For EF Core 2.0+ ,
             *As of March 2019 Microsoft recommends you put your database migration code 
             *in your application entry class but outside of the WebHost build code.
             */

            var host = CreateWebHostBuilder(args).Build();
            //CreateDbIfNotExists(host); //cannot be used with EF migration 
            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var senavmscontext = services.GetRequiredService<SenaVMSContext>();
                    //DbInitializer.Initialize(senavmscontext); //ensurecreated and seed 

                    var authdbcontext = services.GetRequiredService<AuthDbContext>();
                    authdbcontext.Database.EnsureCreated();

                    var persistedgrantdbcontext = services.GetRequiredService<PersistedGrantDbContext>();
                    persistedgrantdbcontext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
            //webBuilder.UseUrls("http://localhost:5000;http://0.0.0.0:5000;http://hostname:5000;");
            webBuilder.UseUrls("http://hostname:5000;");

            webBuilder.UseSerilog((context, configuration) =>
            {
                configuration
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.File(@"identityserver4_log.txt")
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate);
            });
        });
    }
}



