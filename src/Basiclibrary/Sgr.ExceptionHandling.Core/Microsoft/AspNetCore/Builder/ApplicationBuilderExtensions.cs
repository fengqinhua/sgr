using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
        /// 开启自定义异常处理中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSgrExceptionHandler(this IApplicationBuilder app, Action<IExceptionHandlingOptions>? configure = null)
        {
            var exceptionHandlingOptions = app.ApplicationServices.GetRequiredService<IExceptionHandlingOptions>();
            configure?.Invoke(exceptionHandlingOptions);

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }
    }
}
