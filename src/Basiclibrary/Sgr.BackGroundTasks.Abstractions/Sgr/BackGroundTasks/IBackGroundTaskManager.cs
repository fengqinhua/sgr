/**************************************************************
 * 
 * 唯一标识：2d4cdc22-a19f-47be-9952-f2c54954657f
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/1 10:03:51
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Linq;

namespace Sgr.BackGroundTasks
{
    /// <summary>
    ///  后台任务管理器
    /// </summary>
    public interface IBackGroundTaskManager
    {
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
        Task<string> EnqueueAsync<TTask>(
            BackGroundTaskPriority priority = BackGroundTaskPriority.Medium,
            TimeSpan? delay = null,
            int? maxRetryCountOnError = null,
            int? retryIntervalSecond = null,
            CancellationToken cancellationToken = default) where TTask : IBackGroundTask;

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
        Task<string> EnqueueAsync<TTask,TTaskData>(
            TTaskData data,
            BackGroundTaskPriority priority = BackGroundTaskPriority.Medium,
            TimeSpan? delay = null,
            int? maxRetryCountOnError = null,
            int? retryIntervalSecond = null,
            CancellationToken cancellationToken = default) 
            where TTask : IBackGroundTask<TTaskData>
            where TTaskData : class;

    }
}
