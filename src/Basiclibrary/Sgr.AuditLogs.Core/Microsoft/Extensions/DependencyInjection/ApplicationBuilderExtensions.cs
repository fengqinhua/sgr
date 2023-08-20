using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Sgr.AspNetCore.ActionFilters;
using Sgr.AspNetCore.Middlewares;
using System;

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
    }
}
