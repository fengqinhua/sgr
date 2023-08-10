using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sgr.AspNetCore.ActionFilters;
using Sgr.AspNetCore.Middlewares;
using Sgr.AspNetCore.Modules;
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
        /// 开启审计日志中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAuditLog(this IApplicationBuilder app,Action<IAuditLogMiddlewareOptions>? configure = null)
        {
            var middlewareOptions = app.ApplicationServices.GetRequiredService<IAuditLogMiddlewareOptions>();
            configure?.Invoke(middlewareOptions);

            app.UseMiddleware<AuditLogMiddleware>();

            return app;
        }

        /// <summary>
        /// 开启审计日志拦截器
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAuditLogFilters(this IApplicationBuilder app, Action<IAuditLogFilterOptions>? configure = null)
        {
            var auditLogFilterOptions = app.ApplicationServices.GetRequiredService<IAuditLogFilterOptions>();
            configure?.Invoke(auditLogFilterOptions);
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
