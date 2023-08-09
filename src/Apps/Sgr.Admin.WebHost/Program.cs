using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sgr.Admin.WebHost.Extensions;
using Sgr.AuditLog;
using Sgr.Foundation.API.OrgEndpoints;

namespace Sgr.Admin.WebHost
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //ÉèÖÃÈÕÖ¾
            builder.Logging.ClearProviders();
            //builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLogWeb();

            builder.Services.AddSgrCore(builder.Configuration);
            builder.Services.AddSgrMvcCore((services) =>
            {
                services.Replace(ServiceDescriptor.Singleton<IAuditLogContributor, AuditLogContributorAll>());
            });

            builder.Services.AddDbContexts(builder.Configuration);
            builder.Services.AddModules(builder.Environment);
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
            });


            builder.Services.AddSgrEndpoints();
            builder.Services.AddControllers();

            var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}


            app.UseAuditLog((options) =>
            {
                options.IsEnabled = false;
                options.IsIgnoreGetRequests = false;
                options.IsIgnoreAnonymousUsers = false;
                //options.AddIgnoredUrl("/");
            }); //"/WeatherForecast"

            app.UseAuditLogFilters((options) =>
            {
                options.IsEnabled = true;
                options.IsIgnoreGetRequests = false;
                options.IsIgnoreAnonymousUsers = false;
            }); //"/WeatherForecast"


            app.UsePoweredBy(true);
            app.UseModules();

            app.UseHttpsRedirection();
            //app.UseAuthorization();
            app.MapControllers();

            app.UseEndpoints();

            app.Run();
        }
    }
}