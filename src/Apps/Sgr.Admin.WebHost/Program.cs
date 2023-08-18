using MediatR.Behaviors;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Sgr.Admin.WebHost.Extensions;
using Sgr.AspNetCore.AuditLog;
using Sgr.Database;
using Sgr.Utilities;
using System;
using System.IO;
using System.Reflection;
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

            builder.Services.AddDbContexts(builder.Configuration);
            builder.Services.AddMediatR(cfg =>
            { 
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
#if DEBUG
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
#endif
                cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            builder.Services.AddSgrMvcCore(builder.Configuration, builder.Environment, (services) =>
            {
                services.AddControllers().AddJsonOptions(options=>
                {
                    JsonHelper.UpdateJsonSerializerOptions(options.JsonSerializerOptions);
                });
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("sgr", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "SGR API",
                        Description = "An ASP.NET Core Web API for SGR",
                        TermsOfService = new Uri("https://github.com/fengqinhua/sgr"),
                        Contact = new OpenApiContact { Name = "Mapleleaf", Email = "mapleleaf1024@163.com" },
                        License = new OpenApiLicense { Name = "MIT License", Url = new Uri("https://github.com/fengqinhua/sgr/blob/main/LICENSE") }
                    });
        
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"Sgr.Foundation.API.xml"));
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"Sgr.Admin.WebHost.xml"));
                });

                services.Replace(ServiceDescriptor.Singleton<IAuditLogContributor, AuditLogContributorAll>());
            });

            var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}


            app.UseSgrMvcCore((appBuilder) =>
            {
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger(options =>
                    {
                        options.RouteTemplate = "docs/{documentName}/swagger.json";
                    });
                    app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("sgr/swagger.json", "SGR API");
                        options.RoutePrefix = "docs";
                    });
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.UseStaticFiles();
                app.MapControllers();
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