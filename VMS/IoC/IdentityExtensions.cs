using System;
using System.Threading.Tasks;
using Lingkail.VMS.Auth.Web.Data;
using Lingkail.VMS.Auth.Web.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddConfiguredIdentity(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AuthDbContext>(
                o => o.UseNpgsql(configuration.GetConnectionString("AuthDbContext"),
                options => options.SetPostgresVersion(new Version(9, 2))
                ));
            
            services
                .AddIdentity<VmsUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    //TODO: uncomment after some tests
                    //options.Password.RequiredLength = 12; 
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IEmailSender, DummyEmailSender>();
            services.AddSingleton<IBase64QrCodeGenerator, Base64QrCodeGenerator>();
            
            return services;
        }
    }

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
    
    internal class DummyEmailSender : IEmailSender
    {
        private readonly ILogger<DummyEmailSender> _logger;

        public DummyEmailSender(ILogger<DummyEmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _logger.LogWarning("Dummy IEmailSender implementation is being used!!!");
            _logger.LogDebug($"{email}{Environment.NewLine}{subject}{Environment.NewLine}{htmlMessage}");
            return Task.CompletedTask;
        }
    }
}