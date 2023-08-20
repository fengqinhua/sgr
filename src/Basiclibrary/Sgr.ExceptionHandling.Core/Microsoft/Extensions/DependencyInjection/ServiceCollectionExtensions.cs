﻿

using Sgr.AspNetCore.Middlewares;
using Sgr.ExceptionHandling;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加异常
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSgrExceptionHandling(this IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddTransient<IExceptionToErrorInfo, DefaultExceptionToErrorInfo>();

            services.AddSingleton<IExceptionHandlingOptions, ExceptionHandlingOptions>();

            return services;
        }

    }
}
