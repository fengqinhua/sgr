/**************************************************************
 * 
 * 唯一标识：4d466ab2-5f1a-4af7-ae41-12f62717da76
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/2 16:50:40
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    public static class Extensions
    {
        #region IBackGroundTaskManager

        public static Task<string> EnqueueAsync<TTask>(this IBackGroundTaskManager backGroundTaskManager,
            TimeSpan? delay = null,
            CancellationToken cancellationToken = default)
            where TTask : IBackGroundTask
        {
            return backGroundTaskManager.EnqueueAsync<TTask>( BackGroundTaskPriority.Medium, delay, null, null, cancellationToken);
        }

        public static Task<string> EnqueueAsync<TTask, TTaskData>(this IBackGroundTaskManager backGroundTaskManager,
            TTaskData data,
            CancellationToken cancellationToken = default)
            where TTask : IBackGroundTask<TTaskData>
            where TTaskData : class
        {
            return backGroundTaskManager.EnqueueAsync<TTask, TTaskData>(data, BackGroundTaskPriority.Medium, null, null, null, cancellationToken);
        }

        public static Task<string> EnqueueAsync<TTask, TTaskData>(this IBackGroundTaskManager backGroundTaskManager,
            TTaskData data,
            TimeSpan? delay,
            CancellationToken cancellationToken = default)
            where TTask : IBackGroundTask<TTaskData>
            where TTaskData : class
        {
            return backGroundTaskManager.EnqueueAsync<TTask, TTaskData>(data, BackGroundTaskPriority.Medium, delay, null, null, cancellationToken);
        }

        #endregion

        #region IRecurringTaskManager

        public static Task ReStartAsync(this IRecurringTaskManager recurringTaskManager,
            string recurringTaskId,
            CancellationToken cancellationToken = default)
        {
            return recurringTaskManager.ReStartAsync(recurringTaskId, null, null, cancellationToken);
        }

        public static Task ReStartAsync(this IRecurringTaskManager recurringTaskManager,
            string recurringTaskId,
            string cronExpressions,
            CancellationToken cancellationToken = default)
        {
            return recurringTaskManager.ReStartAsync(recurringTaskId, null, cronExpressions, cancellationToken);
        }

        public static Task ReStartAsync(this IRecurringTaskManager recurringTaskManager,
             string recurringTaskId,
             object taskData,
             CancellationToken cancellationToken = default)
        {
            return recurringTaskManager.ReStartAsync(recurringTaskId, taskData, null, cancellationToken);
        }


        #endregion

    }
}
