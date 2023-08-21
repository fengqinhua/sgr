/**************************************************************
 * 
 * 唯一标识：fb8d08a2-4f75-44c3-9913-3aa555a5d29e
 * 命名空间：Microsoft.Extensions.DependencyInjection
 * 创建时间：2023/8/20 11:24:32
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.AuditLogs.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuditCore(this IServiceCollection services)
        {
            services.AddTransient<IAuditLogService, DefaultAuditLogService>();
            return services;
        }
            
    }
}
