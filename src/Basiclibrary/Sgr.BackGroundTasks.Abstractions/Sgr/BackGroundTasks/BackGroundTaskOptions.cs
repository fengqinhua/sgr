/**************************************************************
 * 
 * 唯一标识：1a5077ee-5e29-48bf-ae44-349e1e94e72d
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 12:04:42
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sgr.BackGroundTasks
{
    public class BackGroundTaskOptions
    {
        private readonly Dictionary<string, BackGroundTaskMetadata> _backGroundTaskMetadatas;
        private readonly Dictionary<string, RecurringTaskMetadata> _recurringTaskMetadatas;


        public BackGroundTaskOptions()
        {
            _backGroundTaskMetadatas = new();
            _recurringTaskMetadatas = new();
        }

        /// <summary>
        /// 任务执行失败后，允许重试的最大次数(默认值)
        /// </summary>
        public int MaxRetryCountOnError { get; set; }
        /// <summary>
        /// 任务执行失败后，重试任务的时间间隔（默认值）
        /// </summary>
        public int RetryIntervalSecond { get; set; }

        /// <summary>
        /// 获取后台任务信息
        /// </summary>
        /// <param name="tDataType"></param>
        /// <returns></returns>
        public BackGroundTaskMetadata GetBackGroundTaskMetadata(Type tDataType)
        {
            return GetBackGroundTaskMetadata(tDataType.FullName);
        }
        /// <summary>
        /// 获取后台任务信息
        /// </summary>
        /// <param name="tDataTypeFullName"></param>
        /// <returns></returns>
        /// <exception cref="BackGroundTaskException"></exception>
        public BackGroundTaskMetadata GetBackGroundTaskMetadata(string tDataTypeFullName)
        {
            if (!_backGroundTaskMetadatas.TryGetValue(tDataTypeFullName, out var metadata))
                throw new BackGroundTaskException($"未注册的后台任务，任务类型名称：{tDataTypeFullName}");
            return metadata;
        }

        /// <summary>
        /// 获取周期性运行的后台任务
        /// </summary>
        /// <param name="recurringTaskId"></param>
        /// <returns></returns>
        /// <exception cref="BackGroundTaskException"></exception>
        public RecurringTaskMetadata GetRecurringTaskMetadata(string recurringTaskId)
        {
            if (!_recurringTaskMetadatas.TryGetValue(recurringTaskId, out var metadata))
                throw new BackGroundTaskException($"未注册的周期性任务，任务标识：{recurringTaskId}");
            return metadata;
        }

        /// <summary>
        /// 注册后台任务
        /// </summary>
        /// <typeparam name="TBackGroundTask"></typeparam>
        public void RegistBackGroundTask<TBackGroundTask>()
        {
            RegistBackGroundTask(typeof(TBackGroundTask));
        }

        /// <summary>
        /// 注册后台任务
        /// </summary>
        /// <param name="type"></param>
        public void RegistBackGroundTask(Type type)
        {
            var metadata = new BackGroundTaskMetadata(type);
            _backGroundTaskMetadatas[metadata.TaskClassType.FullName] = metadata;
        }

        /// <summary>
        /// 注册周期性运行的后台任务
        /// </summary>
        /// <typeparam name="TBackGroundTask"></typeparam>
        /// <param name="recurringTaskId"></param>
        /// <param name="recurringTaskName"></param>
        /// <param name="recurringTaskCronExpressions"></param>
        /// <param name="recurringTaskData"></param>
        public void RegistRecurringTask<TBackGroundTask>(
           string recurringTaskId,
           string recurringTaskName,
           string recurringTaskCronExpressions,
           object? recurringTaskData = null)
        {
            Check.StringNotNullOrWhiteSpace(recurringTaskId, nameof(recurringTaskId));
            Check.StringNotNullOrWhiteSpace(recurringTaskName, nameof(recurringTaskName));
            Check.StringNotNullOrWhiteSpace(recurringTaskCronExpressions, nameof(recurringTaskCronExpressions));

            RegistBackGroundTask(typeof(TBackGroundTask));

            RegistRecurringTask(recurringTaskId, recurringTaskName, recurringTaskCronExpressions, typeof(TBackGroundTask), recurringTaskData);
        }

        /// <summary>
        /// 注册周期性运行的后台任务
        /// </summary>
        /// <param name="recurringTaskId"></param>
        /// <param name="recurringTaskName"></param>
        /// <param name="recurringTaskCronExpressions"></param>
        /// <param name="backGroundTaskType"></param>
        /// <param name="recurringTaskData"></param>
        /// <exception cref="BackGroundTaskException"></exception>
        public void RegistRecurringTask(
            string recurringTaskId,
            string recurringTaskName,
            string recurringTaskCronExpressions,
            Type backGroundTaskType,
            object? recurringTaskData = null)
        {
            Check.StringNotNullOrWhiteSpace(recurringTaskId, nameof(recurringTaskId));
            Check.StringNotNullOrWhiteSpace(recurringTaskName, nameof(recurringTaskName));
            Check.StringNotNullOrWhiteSpace(recurringTaskCronExpressions, nameof(recurringTaskCronExpressions));

            if (_recurringTaskMetadatas.ContainsKey(recurringTaskId))
                throw new BackGroundTaskException($"Recurring Task Id : {recurringTaskId} 已存在，不可重复注册");

            BackGroundTaskMetadata backGroundTaskMetadata = GetBackGroundTaskMetadata(backGroundTaskType);

            _recurringTaskMetadatas.Add(recurringTaskId, new RecurringTaskMetadata(recurringTaskId, recurringTaskName, recurringTaskCronExpressions, backGroundTaskMetadata, recurringTaskData));
        }


    }
}
