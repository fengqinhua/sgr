/**************************************************************
 * 
 * 唯一标识：bf1cc646-6648-4440-9f55-9a51e4c9ae80
 * 命名空间：Microsoft.Extensions.DependencyInjection
 * 创建时间：2023/8/20 19:16:33
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr;
using Sgr.AspNetCore.Middlewares;
using System;

namespace Microsoft.AspNetCore.Builder
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
        public static IApplicationBuilder UseSgrWeb(this IApplicationBuilder app,
            Action<WebApplicationUseOptions>? configure = null)
        {
            WebApplicationUseOptions applicationOptions = new();
            configure?.Invoke(applicationOptions);

            //启用并配置审计日志拦截器
            app.UseSgrAuditLogFilters(applicationOptions.AuditLogFilterOptionsConfigure);
            //启用模块化
            app.UseSgrModules();

            //启用异常处理中间件
            if (applicationOptions.EnableExceptionHandler)
            {
                app.UseSgrExceptionHandler(applicationOptions.ExceptionHandlingOptionsConfigure);
            }
            //启用并配置审计日志中间件
            if (applicationOptions.EnableAuditLogMiddleware)
            {
                app.UseSgrAuditLog(applicationOptions.AuditLogMiddlewareOptionsConfigure);
            }

            //启用PoweredBy中间件
            app.UseSgrPoweredBy(applicationOptions.EnablePoweredByMiddleware, applicationOptions.PoweredByHeaderValue);


            return app;
        }


    }
}
