/**************************************************************
 * 
 * 唯一标识：f559f471-62b2-4876-a85a-7b0a9e632708
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 17:06:14
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Quartz;
using Sgr.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sgr.BackGroundTasks
{
    public class QuartzBackGroundTaskManager : IBackGroundTaskManager
    {
        public const string Prefix = "sgr-";

        private readonly ISchedulerFactory _schedulerFactory;

        public QuartzBackGroundTaskManager(ISchedulerFactory factory)
        {
            _schedulerFactory = factory;
        }

        public async Task<string> EnqueueAsync(BackGroundTaskBuilder backGroundTaskBuilder, CancellationToken cancellationToken = default)
        {
            if(backGroundTaskBuilder.HasTaskArgs && backGroundTaskBuilder.TaskArgs == null)
                throw new BackGroundTaskException($"未设置任务 {backGroundTaskBuilder.TaskTypeName} 执行所需的参数!");

            string? dataObject = null;
            if (backGroundTaskBuilder.HasTaskArgs)
                dataObject = JsonHelper.SerializeObject(backGroundTaskBuilder.TaskArgs);

            var job = CreateJobDetail(backGroundTaskBuilder.TaskTypeName, dataObject, backGroundTaskBuilder.MaxRetryCountOnError, backGroundTaskBuilder.RetryIntervalSecond);

            ITrigger trigger = CreateTrigger(backGroundTaskBuilder.Priority, backGroundTaskBuilder.Delay);

            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            await scheduler.ScheduleJob(job, trigger, cancellationToken);

            return job.Key.Name;
        }

        /// <summary>
        /// 添加一个任务（任务无入参）
        /// </summary>
        /// <typeparam name="TTask"></typeparam>
        /// <param name="priority"></param>
        /// <param name="delay"></param>
        /// <param name="maxRetryCountOnError"></param>
        /// <param name="retryIntervalSecond"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async  Task<string> EnqueueAsync<TTask>(
            BackGroundTaskPriority priority = BackGroundTaskPriority.Medium,
            TimeSpan? delay = null,
            int? maxRetryCountOnError = null,
            int? retryIntervalSecond = null,
            CancellationToken cancellationToken = default) where TTask : IBackGroundTask
        {
            var job = CreateJobDetail(typeof(TTask).FullName, null, maxRetryCountOnError, retryIntervalSecond);
            ITrigger trigger = CreateTrigger(priority, delay);

            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            await scheduler.ScheduleJob(job, trigger, cancellationToken);

            return job.Key.Name;
        }

        /// <summary>
        /// 添加一个任务（任务含入参）
        /// </summary>
        /// <typeparam name="TTask"></typeparam>
        /// <typeparam name="TTaskData"></typeparam>
        /// <param name="data"></param>
        /// <param name="priority"></param>
        /// <param name="delay"></param>
        /// <param name="maxRetryCountOnError"></param>
        /// <param name="retryIntervalSecond"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async  Task<string> EnqueueAsync<TTask, TTaskData>(
            TTaskData data,
            BackGroundTaskPriority priority = BackGroundTaskPriority.Medium,
            TimeSpan? delay = null,
            int? maxRetryCountOnError = null,
            int? retryIntervalSecond = null,
            CancellationToken cancellationToken = default)
            where TTask : IBackGroundTask<TTaskData>
            where TTaskData : class
        {
            var job = CreateJobDetail(typeof(TTask).FullName, JsonHelper.SerializeObject(data), maxRetryCountOnError, retryIntervalSecond);
            ITrigger trigger = CreateTrigger(priority, delay);

            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            await scheduler.ScheduleJob(job, trigger, cancellationToken);

            return job.Key.Name;
        }

        private static ITrigger CreateTrigger(BackGroundTaskPriority priority, TimeSpan? delay)
        {
            int priorityValue = GetPriorityValue(priority);

            var trigger = delay.HasValue ? TriggerBuilder.Create().StartAt(DateTimeOffset.UtcNow.Add(delay!.Value)).WithPriority(priorityValue).Build()
                : TriggerBuilder.Create().StartNow().WithPriority(priorityValue).Build();
            return trigger;
        }

        private static int GetPriorityValue(BackGroundTaskPriority priority)
        {
            var priorityValue = priority switch
            {
                BackGroundTaskPriority.High => 10,
                BackGroundTaskPriority.Low => 1,
                BackGroundTaskPriority.Medium => 5,
                _ => 5,
            };
            return priorityValue;
        }

        private static IJobDetail CreateJobDetail(
            string? taskClassType,
            string? dataObject, 
            int? maxRetryCountOnError = null,
            int? retryIntervalSecond = null)
        {
            JobBuilder jobBuilder = JobBuilder.Create<QuartzGenericJob>()
                       .UsingJobData(Prefix + "TaskClassType", taskClassType)
                       .UsingJobData(Prefix + "MaxRetryCountOnError", maxRetryCountOnError.HasValue ? maxRetryCountOnError.Value.ToString() : "")
                       .UsingJobData(Prefix + "RetryIntervalSecond", retryIntervalSecond.HasValue ? retryIntervalSecond.Value.ToString() : "");

            if (string.IsNullOrEmpty(dataObject))
            {
                return jobBuilder.Build();
            }
            else
            {
                return jobBuilder
                    .UsingJobData(Prefix + "TDataObject", dataObject)
                    .Build();
            }
        }
    }
}
