/**************************************************************
 * 
 * 唯一标识：f576ca5e-3131-4d0b-af27-a8d94ed6e944
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/2 17:17:27
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Options;
using Quartz;
using Sgr.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sgr.BackGroundTasks
{
    public class QuartzRecurringTaskManager : IRecurringTaskManager
    {
        public const string RecurringGroupName = "sgr-recurring";

        private readonly ISchedulerFactory _schedulerFactory;
        private readonly BackGroundTaskOptions _backGroundTaskOptions;

        public QuartzRecurringTaskManager(ISchedulerFactory factory,
           IOptions<BackGroundTaskOptions> backGroundTaskOptions)
        {
            _schedulerFactory = factory;
            _backGroundTaskOptions = backGroundTaskOptions.Value;
        }

        /// <summary>
        /// 重启任务（如果任务已存在，那么会先停止再重新启动）
        /// </summary>
        /// <param name="recurringTaskId"></param>
        /// <param name="taskData"></param>
        /// <param name="cronExpressions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ReStartAsync(string recurringTaskId,
            object? taskData = null,
            string? cronExpressions = null,
            CancellationToken cancellationToken = default)
        {
            //获取并更新周期性任务描述信息
            var recurringTaskDefine = _backGroundTaskOptions.GetRecurringTaskMetadata(recurringTaskId);
            
            if (taskData != null)
                recurringTaskDefine.TaskData = taskData;

            if(cronExpressions != null)
                recurringTaskDefine.CronExpressions = cronExpressions;

            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            //先确保任务已停止
            var jobKey = getJobKey(recurringTaskId);
            if (await scheduler.CheckExists(jobKey, cancellationToken))
            {
                await StopAsync(scheduler, recurringTaskId, cancellationToken);

                if (await scheduler.CheckExists(jobKey, cancellationToken))
                    throw new BackGroundTaskException($"停止任务(Task Id = )失败!");
            }

            //启动任务
            var job = JobBuilder.Create<QuartzPeriodicJob>()
                       .WithIdentity(jobKey)
                       .UsingJobData(QuartzBackGroundTaskManager.Prefix + "TaskClassType", recurringTaskDefine.TaskMetadata.TaskClassType.FullName)
                       .UsingJobData(QuartzBackGroundTaskManager.Prefix + "TDataObject", JsonHelper.SerializeObject(taskData))
                       .Build();

            var trigger = (recurringTaskDefine.CronExpressions != null) 
                ? TriggerBuilder.Create().WithIdentity(getTriggerKey(recurringTaskId)).WithCronSchedule(recurringTaskDefine.CronExpressions!).Build()
                :  TriggerBuilder.Create().WithIdentity(getTriggerKey(recurringTaskId)).StartNow().Build();

            await scheduler.ScheduleJob(job, trigger, cancellationToken);
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="recurringTaskId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(string recurringTaskId,
            CancellationToken cancellationToken = default)
        {
            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

            await StopAsync(scheduler, recurringTaskId, cancellationToken);
        }

        protected async Task StopAsync(
            IScheduler scheduler,
            string recurringTaskId,
            CancellationToken cancellationToken = default)
        {
            await scheduler.UnscheduleJob(getTriggerKey(recurringTaskId), cancellationToken);
        }

        private static JobKey getJobKey(string recurringTaskId)
        {
            return new JobKey("Job-" + recurringTaskId, RecurringGroupName);
        }

        private static TriggerKey getTriggerKey(string recurringTaskId)
        {
            return new TriggerKey("Trigger-" + recurringTaskId, RecurringGroupName);
        }


    }
}
