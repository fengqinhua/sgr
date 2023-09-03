/**************************************************************
 * 
 * 唯一标识：7e15ac26-63b5-4452-8c01-7b48cc6f24a3
 * 命名空间：Microsoft.Extensions.DependencyInjection
 * 创建时间：2023/8/20 18:18:36
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sgr;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSgrWeb(this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment,
            Action<WebApplicationAddOptions>? configure = null)
        {
            WebApplicationAddOptions applicationOptions = new();
            configure?.Invoke(applicationOptions);

            services.AddSgrCore(configuration);
            
            services.AddUserIdentity();
            services.AddSgrPoweredBy();
            //缓存
            services.AddSgrCaching(configuration);
            //审计日志
            services.AddSgrAuditLog(applicationOptions.IsAuditFull);
            //异常处理
            services.AddSgrExceptionHandling();     
            //模块化
            services.AddModules(environment, applicationOptions.ModuleInfoProvider);
            //Oss文件管理
            services.AddSgrLocalFileSystemOss(configuration);
            //任务调度
            services.AddSgrBackGroundTasksCore();
            services.AddQuartzBackGroundTasks(configuration);

            return services;
        }
    }
}
