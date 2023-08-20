using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Sgr.AspNetCore.Middlewares;
using Sgr.AspNetCore.Modules;
using System;
using System.Linq;
using System.Reflection.PortableExecutable;

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
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSgrMvcCore(this IApplicationBuilder app, Action<IApplicationBuilder>? configure = null)
        {
            //app.UseAuditLog((options) =>
            //{
            //    options.IsEnabled = true;
            //    options.IsIgnoreGetRequests = false;
            //    options.IsIgnoreAnonymousUsers = false;
            //    //options.AddIgnoredUrl("/");
            //}); 

            //app.UseAuditLogFilters((options) =>
            //{
            //    options.IsEnabled = true;
            //    options.IsIgnoreGetRequests = false;
            //    options.IsIgnoreAnonymousUsers = false;
            //});

            app.UseModules();
//#if DEBUG
//            app.UseSgrExceptionHandler((options) =>
//            {
//                //options.IncludeFullDetails = true;
//                options.IncludeFullDetails = false;
//            });
//#else
//            app.UseSgrExceptionHandler();
//#endif
            app.UsePoweredBy(true);
             
            configure?.Invoke(app);

            return app;
        }


        /// <summary>
        /// 开启PoweredBy中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePoweredBy(this IApplicationBuilder app, bool enabled)
        {
            var options = app.ApplicationServices.GetRequiredService<IPoweredByMiddlewareOptions>();
            options.Enabled = enabled;

            app.UseMiddleware<PoweredByMiddleware>();

            return app;
        }

        /// <summary>
        /// 开启PoweredBy中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="enabled"></param>
        /// <param name="headerValue"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePoweredBy(this IApplicationBuilder app, bool enabled, string headerValue)
        {
            var options = app.ApplicationServices.GetRequiredService<IPoweredByMiddlewareOptions>();
            options.Enabled = enabled;
            options.HeaderValue = headerValue;

            app.UseMiddleware<PoweredByMiddleware>();

            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseModules(this IApplicationBuilder app)
        {
            var modules = app.ApplicationServices.GetServices<IModuleStartup>().OrderBy(f => f.Order);
            var webHostEnvironment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            foreach (var module in modules)
            {
                module.Configure(app, webHostEnvironment);
            }
            return app;
        }
    }
}
