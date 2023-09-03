/**************************************************************
 * 
 * 唯一标识：73c73df0-ab07-4cb3-97d9-95c6c74accc5
 * 命名空间：Microsoft.Extensions.DependencyInjection
 * 创建时间：2023/9/3 7:35:55
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sgr.BackGroundTasks;
using Sgr.Generator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSgrBackGroundTasksCore(this IServiceCollection services/*, IConfiguration configuration*/)
        {
            services.AddSingleton<IBackGroundExceptionHandle, DefaultBackGroundExceptionHandle>();
            services.AddSingleton<IBackGroundTaskExecutor, DefaultBackGroundTaskExecutor>();

            services.AddSingleton<IBackGroundTaskManager, NoBackGroundTaskManager>();
            services.AddSingleton<IRecurringTaskManager, NoRecurringTaskManager>();

            services.AddSingleton<RemoveCacheBackGroundTask>();
            
            services.AddOptions<BackGroundTaskOptions>();
            services.Configure<BackGroundTaskOptions>(opts =>
            {
                opts.RegistBackGroundTask<RemoveCacheBackGroundTask>();
            });
            
            //services.PostConfigure<BackGroundTaskOptions>(opt =>
            //{
            //    opt.RegistBackGroundTask<RemoveCacheBackGroundTask>();
            //});


            return services;
        }
    }
}
