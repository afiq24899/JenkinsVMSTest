using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcExtensions
    {
        public static IServiceCollection AddConfiguredMvc(this IServiceCollection services)
        {
            services
                .AddRazorPages(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account");
                    options.Conventions.AuthorizeFolder("/Consent");
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents();
                options.Cookie.Name = "CloudsCookie";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(3.0);

                /*options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";*/

                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/AccessDenied";
            });

            services
                .AddControllersWithViews();

            return services;

        }
    }
}


