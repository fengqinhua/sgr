using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sgr.Modules;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
        {
            app.UseFastEndpoints();
            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseModules(this IApplicationBuilder app)
        {
            var modules = app.ApplicationServices.GetServices<IModuleStartup>().OrderBy(f=>f.Order);
            var webHostEnvironment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            foreach (var module in modules)
            {
                module.Configure(app, webHostEnvironment);
            }
            return app;
        }
    }
}
