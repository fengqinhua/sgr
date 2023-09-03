/**************************************************************
 * 
 * 唯一标识：dd777383-cb9e-432e-b32c-5b8e5d70b48f
 * 命名空间：Microsoft.Extensions.DependencyInjection
 * 创建时间：2023/9/3 7:41:14
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
using Sgr.BackGroundTasks;
using Sgr.Caching.Services;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQuartzBackGroundTasks(this IServiceCollection services, IConfiguration configuration)
        {
            //Quartz
            services.Configure<QuartzOptions>(configuration.GetSection("Quartz"));

            services.Configure<QuartzOptions>(options =>
            {
                options.Scheduling.IgnoreDuplicates = true; // default: false
                options.Scheduling.OverWriteExistingData = true; // default: true
            });

            services.AddQuartz(q =>
            {
                //调度器标识
                q.SchedulerId = "Scheduler-Core";

                // 前面已通过 Configure<QuartzOptions> 函数，按照 appsettings.json 中的值进行了设置
                // q.SchedulerName = "Sgr Quartz ASP.NET Core Scheduler";

                // 当调度器结束时，是否中断正在运行的任务
                q.InterruptJobsOnShutdown = true;

                // 当QuartzHostedServiceOptions.WaitForJobsToComplete = true or scheduler.Shutdown(waitForJobsToComplete: true)
                // 当调度器结束时,如果中断正在运行的任务，那么则等待任务完成
                q.InterruptJobsOnShutdownWithWait = true;

                // 一次运行的最大任务数
                q.MaxBatchSize = 5;

                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();                               // 使用内存存储Job
                q.UseDefaultThreadPool(maxConcurrency: 10);         // 配置Quartz默认的线程池大小

            });

            services.AddQuartzHostedService(q =>
            {
                q.WaitForJobsToComplete = true;
            });

            //Sgr
            if (services.Any(f => f.ServiceType == typeof(IBackGroundTaskManager)))
                services.Replace(ServiceDescriptor.Singleton<IBackGroundTaskManager, QuartzBackGroundTaskManager>());
            else
                services.AddSingleton<IBackGroundTaskManager, QuartzBackGroundTaskManager>();

            if (services.Any(f => f.ServiceType == typeof(IRecurringTaskManager)))
                services.Replace(ServiceDescriptor.Singleton<IRecurringTaskManager, QuartzRecurringTaskManager>());
            else
                services.AddSingleton<IRecurringTaskManager, QuartzRecurringTaskManager>();


            return services;
        }
    }
}
