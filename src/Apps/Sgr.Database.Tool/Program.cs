using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sgr.Database.Tool.Extensions;
using Sgr.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Sgr.Database.Tool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            //设置日志
            builder.Logging.ClearProviders();
            //builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLogWeb();

            
            builder.Services.AddSgrCore(builder.Configuration);
            builder.Services.AddSgrMvcCore();
            builder.Services.AddModules(builder.Environment);
            builder.Services.AddDbContexts(builder.Configuration);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
            });

            var app = builder.Build();

            app.UseModules();


            using (var scope = app.Services.CreateScope())
            {
                var providers = scope.ServiceProvider.GetServices<IEntityFrameworkTypeProvider>();

                var dbContext = scope.ServiceProvider.GetRequiredService<SgrDbContext>();
                //dbContext.Database.EnsureCreated();
                //var context = scope.ServiceProvider.GetRequiredService<OrderingContext>();
                //var env = app.Services.GetService<IWebHostEnvironment>();
                //var settings = app.Services.GetService<IOptions<OrderingSettings>>();
                //var logger = app.Services.GetService<ILogger<OrderingContextSeed>>();
                //await context.Database.MigrateAsync();

                //await new OrderingContextSeed().SeedAsync(context, env, settings, logger);
                //var integEventContext = scope.ServiceProvider.GetRequiredService<IntegrationEventLogContext>();
                //await integEventContext.Database.MigrateAsync();
            }

            Console.WriteLine("work completed !");
            Console.ReadLine();
        }
    }
}