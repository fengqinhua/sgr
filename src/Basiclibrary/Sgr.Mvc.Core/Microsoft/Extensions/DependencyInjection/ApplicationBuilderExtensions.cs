using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sgr.ActionFilters;
using Sgr.AuditLog;
using Sgr.Middlewares;
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
        /// 开启审计日志中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="enabled">是否启用</param>
        /// <param name="ignoredUrls"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAuditLog(this IApplicationBuilder app, bool enabled = true, string[]? ignoredUrls = null )
        {
            var middlewareOptions = app.ApplicationServices.GetRequiredService<IAuditLogMiddlewareOptions>();
            middlewareOptions.IsEnabled = enabled;

            if (ignoredUrls != null)
            {
                middlewareOptions.ClearIgnoredUrls();
                foreach(var ignoredUrl in ignoredUrls)
                {
                    middlewareOptions.AddIgnoredUrl(ignoredUrl);
                }
            }
            app.UseMiddleware<AuditLogMiddleware>();

            return app;
        }

        /// <summary>
        /// 开启审计日志拦截器
        /// </summary>
        /// <param name="app"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAuditLogFilters(this IApplicationBuilder app, bool enabled = true)
        {
            var auditLogFilterOptions = app.ApplicationServices.GetRequiredService<IAuditLogFilterOptions>();
            auditLogFilterOptions.IsEnabled = enabled;
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
