using Microsoft.AspNetCore.Http;
using Sgr.AspNetCore.ActionFilters;
using Sgr.AspNetCore.Middlewares;
using Sgr.AuditLogs;
using Sgr.AuditLogs.Contributor;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加审计日志相关服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSgrAuditLog(this IServiceCollection services)
        {
            services.AddSingleton<IEnableBufferingOptions, EnableBufferingOptions>();

            services.AddSingleton<IAuditLogMiddlewareOptions, AuditLogMiddlewareOptions>();
            services.AddSingleton<IAuditLogFilterOptions, AuditLogFilterOptions>();

            
            services.AddSingleton<IHttpUserAgentProvider, DefaultHttpUserAgentProvider>();
            services.AddSingleton<IAuditLogContributor, AuditLogContributor>();
         
            services.AddTransient<AuditLogMiddleware>();

            services.AddTransient<AuditLogActionFilterAttribute>();
            services.AddTransient<AuditLogPageFilterAttribute>();
            services.AddTransient<UnAuditLogActionFilterAttribute>();
            services.AddTransient<UnAuditLogPageFilterAttribute>();

            return services;
        }

    }
}
