using FastEndpoints;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sgr.Generator;
using Sgr.Modules;
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
