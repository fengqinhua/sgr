using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Sgr.AspNetCore.ActionFilters;
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
            //    options.IsEnabled = false;
            //    options.IsIgnoreGetRequests = false;
            //    options.IsIgnoreAnonymousUsers = false;
            //    //options.AddIgnoredUrl("/");
            //}); //"/WeatherForecast"

            app.UseAuditLogFilters((options) =>
            {
                options.IsEnabled = true;
                options.IsIgnoreGetRequests = false;
                options.IsIgnoreAnonymousUsers = false;
            }); //"/WeatherForecast"



            app.UseModules();
            app.UseSgrExceptionHandler();
            app.UsePoweredBy(true);
             
            configure?.Invoke(app);

            return app;
        }

        /// <summary>
        /// 开启自定义移除处理中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSgrExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }

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

            //解决在审计日志中读取读取HttpBody的问题
            //参考;https://blog.csdn.net/hwt0101/article/details/80893212
            if (auditLogFilterOptions.IsEnabled && auditLogFilterOptions.Contributor.IsAuditFull)
                app.UseEnableBuffering();

            return app;
        }

        public static IApplicationBuilder UseEnableBuffering(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetRequiredService<IEnableBufferingOptions>();

            if (!options.IsUsed)
            {
                app.Use(next => context =>
                {
                    context.Request.EnableBuffering();
                    return next(context);
                });

                options.IsUsed = true;
            }

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
