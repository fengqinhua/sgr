/**************************************************************
 * 
 * 唯一标识：eeef0848-e39e-4ae4-95d4-76dd033870e1
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/7 15:56:35
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace Sgr.BackGroundTasks
{
    public sealed class BackGroundTaskBuilder
    {
        private BackGroundTaskBuilder(string taskTypeName, bool hasTaskArgs = false, string? taskArgsTypeName = null)
        {
            TaskTypeName = taskTypeName;
            HasTaskArgs = hasTaskArgs;
            TaskArgsTypeName = taskArgsTypeName;
        }

        /// <summary>
        /// 后台任务的类型名称
        /// </summary>
        public string TaskTypeName { get; internal set; }
        /// <summary>
        /// 是否包含入参
        /// </summary>
        public bool HasTaskArgs { get; internal set; }
        /// <summary>
        /// 入参类型
        /// </summary>
        public string? TaskArgsTypeName { get; internal set; }
        /// <summary>
        /// 入参值
        /// </summary>
        public object? TaskArgs { get; internal set; }
        /// <summary>
        /// 任务优先级
        /// </summary>
        public BackGroundTaskPriority Priority { get; internal set; } = BackGroundTaskPriority.Medium;
        /// <summary>
        /// 任务延迟多久执行
        /// </summary>
        public TimeSpan? Delay { get; internal set; }
        /// <summary>
        /// 任务执行异常时，最大重试次数
        /// </summary>
        public int? MaxRetryCountOnError { get; internal set; }
        /// <summary>
        /// 任务执行异常时，重试的时间间隔
        /// </summary>
        public int? RetryIntervalSecond { get; internal set; }

        /// <summary>
        /// 设置入参（如果需要）
        /// </summary>
        /// <param name="obj"></param>
        public BackGroundTaskBuilder WithArgs(object obj)
        {
            TaskArgs = obj;
            return this;
        }

        /// <summary>
        /// 设置任务优先级
        /// </summary>
        /// <param name="priority"></param>
        /// <returns></returns>
        public BackGroundTaskBuilder WithPriority(BackGroundTaskPriority priority)
        {
            Priority = priority;
            return this;
        }

        /// <summary>
        /// 设置任务延迟多久执行
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public BackGroundTaskBuilder WithDelay(TimeSpan delay)
        {
            Delay = delay;
            return this;
        }

        /// <summary>
        /// 任务执行异常时，最大重试次数
        /// </summary>
        /// <param name="maxRetryCountOnError"></param>
        /// <returns></returns>
        public BackGroundTaskBuilder WithMaxRetryCountOnError(int maxRetryCountOnError)
        {
            if(maxRetryCountOnError < 0) { maxRetryCountOnError = 0; }

            MaxRetryCountOnError = maxRetryCountOnError;
            return this;
        }

        /// <summary>
        /// 任务执行异常时，重试的时间间隔
        /// </summary>
        /// <param name="retryIntervalSecond"></param>
        /// <returns></returns>
        public BackGroundTaskBuilder WithRetryIntervalSecond(int retryIntervalSecond)
        {
            if (retryIntervalSecond < 0) { retryIntervalSecond = 0; }

            RetryIntervalSecond = retryIntervalSecond;
            return this;
        }

        public static BackGroundTaskBuilder Create<TTask>()
        {
            return Create(typeof(TTask));
        }

        public static BackGroundTaskBuilder Create(Type taskClassType)
        {
            if (taskClassType == null)
                throw new BackGroundTaskException("后台任务的类型不可为空");

            //IBackGroundTask
            if (typeof(IBackGroundTask).IsAssignableFrom(taskClassType))
            {
                return new BackGroundTaskBuilder(taskClassType.FullName);
            }

            //IBackGroundTask<TData>
            foreach (Type item in taskClassType.GetInterfaces())
            {
                if (!item.IsGenericType
                    || item.GetGenericTypeDefinition() != typeof(IBackGroundTask<>))
                    continue;

                var genericArguments = item.GetGenericArguments();
                if (genericArguments.Length == 1)
                {
                    return new BackGroundTaskBuilder(taskClassType.FullName,true, genericArguments[0].FullName);
                }
            }

            throw new BackGroundTaskException($"TaskClassType ({taskClassType.FullName}) 未实现 IBackGroundTask 或 IBackGroundTask<TData> 接口");
        }




    }
}
