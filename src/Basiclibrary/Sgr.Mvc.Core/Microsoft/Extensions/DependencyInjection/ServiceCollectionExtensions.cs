﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Sgr;
using Sgr.AspNetCore;
using Sgr.AspNetCore.Middlewares;
using Sgr.AspNetCore.Modules;
using Sgr.Caching.Services;
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
            services.AddTransient<ICurrentUser, DefaultCurrentUser>();
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
