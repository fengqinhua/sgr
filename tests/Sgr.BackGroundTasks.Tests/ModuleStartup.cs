/**************************************************************
 * 
 * 唯一标识：daf62161-4dc2-4b09-9079-567336459a4f
 * 命名空间：Sgr.UPMS.API
 * 创建时间：2023/8/20 17:48:52
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sgr.Modules;

namespace Sgr.BackGroundTasks.Tests
{
    public class ModuleStartup : ModuleStartupBase
    {
        public const string TimerBackGroundTaskId = "TimerBackGroundTask";

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //启动周期性任务
            var recurringTaskManager = app.ApplicationServices.GetRequiredService<IRecurringTaskManager>();
            recurringTaskManager.ReStartAsync(TimerBackGroundTaskId);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<WriteReadBackGroundTask>();
            services.AddSingleton<CounterBackGroundTask>();
            services.AddSingleton<TimerBackGroundTask>(); 

            services.Configure<BackGroundTaskOptions>(opts =>
            {
                opts.RegistBackGroundTask<WriteReadBackGroundTask>();
                opts.RegistBackGroundTask<CounterBackGroundTask>();
                opts.RegistRecurringTask<TimerBackGroundTask>(TimerBackGroundTaskId, "每秒更新一次时间", "0/2 * * * * ?");
            });
        }
    }
}
