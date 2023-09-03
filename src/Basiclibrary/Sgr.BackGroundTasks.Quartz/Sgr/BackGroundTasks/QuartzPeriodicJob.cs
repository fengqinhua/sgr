/**************************************************************
 * 
 * 唯一标识：a8f6a11e-ed3c-4683-b46a-6fd4f710c2da
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/1 6:30:54
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Sgr.Utilities;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sgr.BackGroundTasks
{
    public class QuartzPeriodicJob : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IBackGroundTaskExecutor _backGroundTaskExecutor;
        private readonly BackGroundTaskOptions _backGroundTaskOptions;
        private readonly IBackGroundExceptionHandle _backGroundExceptionHandle;

        private readonly ILogger<QuartzPeriodicJob> _logger;

        public QuartzPeriodicJob(IServiceScopeFactory serviceScopeFactory, IBackGroundTaskExecutor backGroundTaskExecutor, IOptions<BackGroundTaskOptions>  backGroundTaskOptions, IBackGroundExceptionHandle backGroundExceptionHandle, ILogger<QuartzPeriodicJob> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _backGroundTaskExecutor = backGroundTaskExecutor;
            _backGroundTaskOptions = backGroundTaskOptions.Value;
            _backGroundExceptionHandle = backGroundExceptionHandle;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var taskClassTypeName = context.MergedJobDataMap.GetString(QuartzBackGroundTaskManager.Prefix + "TaskClassType") ?? "null";
            var taskMetadata = _backGroundTaskOptions.GetBackGroundTaskMetadata(taskClassTypeName) ?? throw new BackGroundTaskException($"类型名称：{taskClassTypeName} 未注册");
            
            object? inputData = null;
            if (taskMetadata.HasInputParameter)
            {
                //如果无法获取执行任务所需的数据，则退出
                string? str_data = context.MergedJobDataMap.GetString(QuartzBackGroundTaskManager.Prefix + "TDataObject");
                if (string.IsNullOrEmpty(str_data))
                {
                    _logger.LogError($"任务 QuartzPeriodicJob 无法从 MergedJobDataMap 中获取任务所需的数据 [ Task Name = {taskMetadata.TaskName} ]，放弃执行 ...");
                    return;
                }

                inputData = JsonHelper.DeserializeObject(taskMetadata.TaskDataType!, str_data);
                if (inputData == null)
                {
                    _logger.LogError($"任务 QuartzPeriodicJob 无法从 MergedJobDataMap 中获取任务所需的数据 [ Task Name = {taskMetadata.TaskName} ]，放弃执行 ...");
                    return;
                }
            }

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var backGroundTaskContext = new BackGroundTaskContext(scope.ServiceProvider, taskMetadata.TaskClassType, inputData);
                try
                {
                    await _backGroundTaskExecutor.ExecuteAsync(backGroundTaskContext);
                }
                catch (Exception exceptionOnTask)
                {
                    _logger.LogError(exceptionOnTask, $"任务 QuartzPeriodicJob 调用任务的具体实现类失败...");

                    await _backGroundExceptionHandle.OnTaskFailAsync(backGroundTaskContext, context.RefireCount);

                    ////检查当前任务是否超出已设置的允许最大重试次数
                    //var strMaxRetryCountOnError = context.MergedJobDataMap.GetString(QuartzBackGroundTaskManager.Prefix + "MaxRetryCountOnError");
                    //if (!int.TryParse(strMaxRetryCountOnError, out int maxRetryCountOnError) || maxRetryCountOnError <= 0)
                    //    maxRetryCountOnError = _backGroundTaskOptions.MaxRetryCountOnError;

                    //if (context.RefireCount > maxRetryCountOnError)
                    //{
                    //    //任务连续失败指定次数，则可认为任务将不可能成功,那么需要进行特殊处理
                    //    _logger.LogError($"任务 QuartzPeriodicJob 已执行失败超过设定的最大上限 [{maxRetryCountOnError}] ..." +
                    //        $"-- 参数值：{str_data}");
                    //    try
                    //    {
                    //        await _backGroundExceptionHandle.OnTaskNeverSucceedAsync(taskClassTypeName, str_data);
                    //    }
                    //    catch (Exception exceptionOnReport)
                    //    {
                    //        _logger.LogError(exceptionOnReport, $"任务 QuartzPeriodicJob 执行 OnJobNeverSucceed 方法失败...");
                    //    }
                    //    return;
                    //}

                    ////如果未超出最大重试次数，则启动重试
                    //var strRetryIntervalSecond = context.MergedJobDataMap.GetString(QuartzBackGroundTaskManager.Prefix + "RetryIntervalSecond");
                    //if (!int.TryParse(strRetryIntervalSecond, out int retryIntervalSecond))
                    //    retryIntervalSecond = _backGroundTaskOptions.RetryIntervalSecond;

                    //var oldTrigger = context.Trigger;
                    //var newTrigger = TriggerBuilder.Create()
                    //    .WithIdentity($"{oldTrigger.Key.Name}-retry", oldTrigger.Key.Group)
                    //    .StartAt(DateTimeOffset.UtcNow.AddSeconds(retryIntervalSecond))
                    //    .Build();

                    //await context.Scheduler.ScheduleJob(newTrigger);
                }
            }
        }
    }
}
