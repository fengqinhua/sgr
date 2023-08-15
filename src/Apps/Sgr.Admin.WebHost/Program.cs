using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using Sgr.Admin.WebHost.Extensions;
using Sgr.AspNetCore.AuditLog;
using Sgr.Database;
using Sgr.EntityFrameworkCore;
using Sgr.Foundation.API.OrgEndpoints;
using System.Linq;
using System.Threading.Tasks;

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
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //设置日志
            builder.Logging.ClearProviders();
            //builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLogWeb();

            builder.Services.AddSgrMvcCore(builder.Configuration, builder.Environment, (services) =>
            {
                services.Replace(ServiceDescriptor.Singleton<IAuditLogContributor, AuditLogContributorAll>());
            });

            builder.Services.AddDbContexts(builder.Configuration);
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSgrMvcCore((appBuilder) =>
            {

            });

            //当处于开发者模式下，程序自动执行数据库初始化逻辑，确保数据库相关表和数据存在
            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    IDatabaseSeed databaseSeed = scope.ServiceProvider.GetRequiredService<IDatabaseSeed>();
                    await databaseSeed.SeedAsync();
                }
            }


            await app.RunAsync();
        }
    }
}