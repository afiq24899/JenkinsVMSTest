using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using System.IO;
//-----------------------------------//
using Lingkail.VMS.Data;
using Lingkail.VMS.Services;
using Lingkail.VMS.Auth.Web.Configuration;
//-------------HUB-------------------//
using Lingkail.VMS.SignalRChat.Hubs;
//----------------------------------//
using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.MemoryStorage;
using Lingkail.VMS.Controllers;
using Lingkail.VMS.Hubs;
using Hangfire.Storage;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
//----------------------------------//

namespace Lingkail.VMS
{
    public class Startup
    {
        //public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region //--------------------LOGIN--------------------//
            services
                //     .AddConfiguredLocalization()
                .AddConfiguredIdentity(Configuration)
                .AddConfiguredMvc();
                //     .AddConfiguredIdentityServer(Environment, Configuration);






            var dataProtectionKeysLocation =
                Configuration.GetSection<DataProtectionSettings>(nameof(DataProtectionSettings))?.Location;
            if (!string.IsNullOrWhiteSpace(dataProtectionKeysLocation))
            {
                services
                    .AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo(dataProtectionKeysLocation));
                // TODO: encrypt the keys
            }
            #endregion

            #region //--------------------POSTGRESQL--------------------//
            /*services.AddEntityFrameworkNpgsql().AddDbContext<SenaVMSContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("SenaVMSContext")));*/
            //Reference: https://www.npgsql.org/efcore/

            /*services.AddEntityFrameworkNpgsql()
                .AddDbContext<SenaVMSContext>(
                o => o.UseNpgsql(("Username=postgres; Password=password; Server=192.168.84.100; Database=senaVMS2019"),
                options => options.SetPostgresVersion(new Version(9, 2))
                )); //For PostgreSQL Version < 10
                    //Reference https://www.npgsql.org/efcore/release-notes/3.0.html*/

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<SenaVMSContext>(
                o => o.UseNpgsql(Configuration.GetConnectionString("SenaVMSContext"),
                options => options.SetPostgresVersion(new Version(9, 2))
                )); //For PostgreSQL Version < 10
                    //Reference https://www.npgsql.org/efcore/release-notes/3.0.html
            #endregion

            services.AddSignalR();
            services.AddControllers();

            //Transient
            services.AddTransient<FileManagement>();
            services.AddTransient<CreateVsnService>();
            services.AddTransient<ColorlightServices>();
            services.AddTransient<DotnetHubInvoker>();
            services.AddTransient<DatabaseAPIServices.ForIncident>();
            services.AddTransient<DatabaseAPIServices.ForParking>();
            services.AddTransient<DatabaseAPIServices.ForTravelTime>();
            services.AddTransient<DatabaseAPIServices.ForVidBroadcast>();
            services.AddTransient<DatabaseAPIServices.ForWeather>();
            services.AddTransient<JsonFileLocationService>();
            services.AddTransient<ITaskSchedule, TaskSchedule>();
            services.AddTransient<DynamicDataService>();

            //HttpContextAccessor
            services.AddHttpContextAccessor();

            //Hangfire
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage()
            );
          //  services.AddHangfireServer();
            /*services.AddHangfire(config =>
                config.UsePostgreSqlStorage(Configuration.GetConnectionString("SenaVMSContext")));*/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
           /* if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else

            {
                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                //app.UseHttpsRedirection(); // if a request comes in HTTP, it's redirect to HTTPS  
            }*/


            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            }); //IMPORTANT: Put BEFORE calling UseAuthentication or other authentication scheme middlewares.

            app.UseHsts();
            //app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions()  //wwwroot
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });

            //if (Environment.IsProduction())
            //{
            //    app.UseStaticFiles(new StaticFileOptions()
            //    {
            //        FileProvider = new PhysicalFileProvider(@"/home/senav/vms_images/"), //senaV
            //        RequestPath = new PathString("/sharingfolder"), //the relative path in the URL which maps to the static folder.
            //    });
            //}
            //else if (Environment.IsDevelopment())
            //{
            //    app.UseStaticFiles(new StaticFileOptions()
            //    {
            //        FileProvider = new PhysicalFileProvider(@"C:\Users\claud\Desktop"), // the folder from which static files will be served
            //        RequestPath = new PathString("/sharingfolder"), //the relative path in the URL which maps to the static folder.
            //    });
            //}
            //else if (Environment.IsEnvironment("StagingCentos7"))
            //{
            //    app.UseStaticFiles(new StaticFileOptions()
            //    {
            //        FileProvider = new PhysicalFileProvider(@"/root/Documents/AlibabaFolder"), //stagingCentos7
            //        RequestPath = new PathString("/sharingfolder"), //the relative path in the URL which maps to the static folder.
            //    });
            //}

            app.UseRouting();
   //         app.UseIdentityServer();
            // good reading on Authentification and Authorization on
            //https://visualstudiomagazine.com/articles/2019/10/29/aspnet-authentication.aspx
            app.UseAuthentication();//this must appear between UserRouting and UseEndpoints
            app.UseAuthorization();
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            //app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();//This only maps controllers that are decorated with routing attributes - it doesn't configure any conventional routes
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllerRoute(
                //name: "default",
                //pattern: "{controller=~/}/{action=Index}/{id?}");
                //endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapHub<BroadcastHub>("/broadcastHub");
            });


            //Hangfire --------------------------------------------------
            /*using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }*/

            app.UseHangfireDashboard(); //Enable dashboard

            app.UseHangfireServer();

            backgroundJobClient.Enqueue(() => Console.WriteLine("\n-----Start Hangfire Tasks-----\n"));

            /*recurringJobManager.AddOrUpdate(
                "[TEST] UPDATE DYNAMIC DATA.", //Name of the job
                () => serviceProvider.GetService<DynamicDataService>().AutoUpdateRandDynamicData(141), //The actual job
                "* * * * *" //Recurring job every 1 min(s) 
                );*/

            recurringJobManager.AddOrUpdate(
                "Update board power status in PSQL", //Name of the job
                () => serviceProvider.GetService<ColorlightServices>().GetAllPowerStatus(), //The actual job
                "* * * * *" //Recurring job every 1 min(s) - see GetPowerStatus, timeout 100ms per board
                );

            BackgroundJob.Enqueue<ITaskSchedule>(TaskSchedule => TaskSchedule.Setdefaultvalue());
            //BackgroundJob.Enqueue<ITaskSchedule>(Setdefaultvalue());
            recurringJobManager.AddOrUpdate(
                "Update up&down time every min",
                () => serviceProvider.GetService<ITaskSchedule>().UpdateStatusBoardAsync(),
               "* * * * *");


            //Check status 
            using (var connection = JobStorage.Current.GetConnection())
            {
                Console.WriteLine("\nRecurring Jobs List\n------------------");
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    Console.WriteLine("JobId: " + recurringJob.Id + "\nJob: " + recurringJob.Job);
                }
                Console.WriteLine("------------------\n");

            }

        }
    }
}
