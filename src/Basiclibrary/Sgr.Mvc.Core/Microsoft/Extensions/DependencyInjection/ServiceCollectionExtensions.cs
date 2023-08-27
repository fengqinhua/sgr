using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Sgr;
using Sgr.AspNetCore;
using Sgr.AspNetCore.ActionFilters.AuditLogs;
using Sgr.AspNetCore.Middlewares.AuditLogs;
using Sgr.AspNetCore.Middlewares.ExceptionHandling;
using Sgr.AspNetCore.Middlewares.PoweredBy;
using Sgr.AuditLogs.Contributor;
using Sgr.AuditLogs;
using Sgr.Caching.Services;
using Sgr.ExceptionHandling;
using Sgr.Modules;
using System;
using System.Linq;
using System.Reflection;
using Sgr.Identity.Services;
using Sgr.Security;
using Sgr.Security.AuthorizationHandlers;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        
        public static IServiceCollection AddSgrMvcCore(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment, Action<IServiceCollection>? configure = null)
        {
            services.AddSgrCore(configuration);

            AddUserIdentity(services);
            AddSgrPoweredBy(services);
            AddModules(services, environment);

            configure?.Invoke(services);
     
            return services;
        }

        public static IServiceCollection AddSgrCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.TryAddSingleton<ICacheManager, MemoryCacheManager>();

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
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUserIdentity(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentUser, DefaultCurrentUser>();

            services.AddScoped<IFunctionPermissionGrantingService, DefaultFunctionPermissionGrantingService>();
            services.AddScoped<IAuthorizationHandler, FunctionPermissionHandler>();

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

        /// <summary>
        /// 添加异常
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSgrExceptionHandling(this IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddSingleton<IExceptionToErrorInfo, DefaultExceptionToErrorInfo>();
            services.AddSingleton<IExceptionHandlingOptions, ExceptionHandlingOptions>();

            return services;
        }

        /// <summary>
        /// 添加审计日志相关服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="isAuditFull"></param>
        /// <returns></returns>
        public static IServiceCollection AddSgrAuditLog(this IServiceCollection services, bool isAuditFull)
        {
            services.AddSingleton<IEnableBufferingOptions, EnableBufferingOptions>();

            services.AddSingleton<IAuditLogMiddlewareOptions, AuditLogMiddlewareOptions>();
            services.AddSingleton<IAuditLogFilterOptions, AuditLogFilterOptions>();


            services.AddSingleton<IHttpUserAgentProvider, DefaultHttpUserAgentProvider>();
            if (isAuditFull)
                services.AddSingleton<IAuditLogContributor, AuditLogContributor>();
            else
                services.AddSingleton<IAuditLogContributor, AuditLogContributorFull>();

            services.AddTransient<AuditLogMiddleware>();

            //services.AddTransient<AuditLogActionFilterAttribute>();
            //services.AddTransient<AuditLogPageFilterAttribute>();
            //services.AddTransient<UnAuditLogActionFilterAttribute>();
            //services.AddTransient<UnAuditLogPageFilterAttribute>();

            return services;
        }
    }
}
