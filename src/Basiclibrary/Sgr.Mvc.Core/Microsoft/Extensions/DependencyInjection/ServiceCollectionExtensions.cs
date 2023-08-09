using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Sgr;
using Sgr.ActionFilters;
using Sgr.AuditLog;
using Sgr.Domain.Entities.Auditing;
using Sgr.Generator;
using Sgr.Middlewares;
using Sgr.Modules;
using Sgr.Services;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddSgrMvcCore(this IServiceCollection services, Action<IServiceCollection>? configure = null)
        {
            AddUserIdentity(services);
            AddSgrPoweredBy(services);
            AddSgrAuditLog(services);

            configure?.Invoke(services);

            return services;
        }

        /// <summary>
        /// 添加PoweredBy相关服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSgrPoweredBy(this IServiceCollection services)
        {
            services.AddSingleton<IPoweredByMiddlewareOptions, PoweredByMiddlewareOptions>();
            //services.AddTransient<PoweredByMiddleware>();

            return services;
        }


        /// <summary>
        /// 添加审计日志相关服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSgrAuditLog(this IServiceCollection services)
        {
            services.AddSingleton<IAuditLogMiddlewareOptions, AuditLogMiddlewareOptions>();
            services.AddSingleton<IAuditLogFilterOptions, AuditLogFilterOptions>();

            services.AddSingleton<IAuditLogContributor, AuditLogContributor>();
         
            services.AddTransient<AuditLogMiddleware>();

            services.AddTransient<AuditLogActionFilterAttribute>();
            services.AddTransient<AuditLogPageFilterAttribute>();
            services.AddTransient<UnAuditLogActionFilterAttribute>();
            services.AddTransient<UnAuditLogPageFilterAttribute>();


            return services;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUserIdentity(this IServiceCollection services)
        {
            services.AddTransient<ICurrentUser, DefaultCurrentUser>();
            return services;
        }

        /// <summary>
        /// 添加 Endpoints
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSgrEndpoints(this IServiceCollection services)
        {
            services.AddFastEndpoints();

            return services;
        }

        /// <summary>
        /// 添加模块化支持
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environment"></param>
        /// <param name="moduleInfoProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddModules(this IServiceCollection services, IHostEnvironment environment, IModuleInfoProvider? moduleInfoProvider = null)
        {
            IModuleInfoProvider provider = moduleInfoProvider ?? new AppConfigurationModuleInfoProvider();
            
            IApplicationInfoContext applicationInfoContext = new ModularApplicationInfoContext(environment, provider);
            services.AddSingleton(applicationInfoContext);



            foreach (var module in applicationInfoContext.Application.Modules)
            {
                if (module.Assembly == null)
                    continue;

                //builder.Services.AddControllers()
                //    .AddApplicationPart(Assembly.Load(new AssemblyName(AssemblyName)));
                //services.AddControllers().AddApplicationPart(module.Assembly);

                var moduleStartupType = module.Assembly.GetTypes().FirstOrDefault(t => typeof(IModuleStartup).IsAssignableFrom(t));
                if ((moduleStartupType != null) && (moduleStartupType != typeof(IModuleStartup)))
                {
                    if (Activator.CreateInstance(moduleStartupType) is IModuleStartup moduleStartup)
                    {
                        services.AddSingleton(typeof(IModuleStartup), moduleStartup);
                        moduleStartup.ConfigureServices(services);
                    }
                }
            }

            return services;
        }

    }
}
