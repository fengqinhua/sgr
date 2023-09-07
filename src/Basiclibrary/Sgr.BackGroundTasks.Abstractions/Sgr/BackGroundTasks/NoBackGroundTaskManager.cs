/**************************************************************
 * 
 * 唯一标识：6e918c68-48ec-4eee-a3e8-90632a2ab263
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/3 7:38:25
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
    public class NoBackGroundTaskManager : IBackGroundTaskManager
    {
        public Task<string> EnqueueAsync<TTask>(BackGroundTaskPriority priority = BackGroundTaskPriority.Medium, TimeSpan? delay = null, int? maxRetryCountOnError = null, int? retryIntervalSecond = null, CancellationToken cancellationToken = default) where TTask : IBackGroundTask
        {
            throw new NotImplementedException();
        }

        public Task<string> EnqueueAsync<TTask, TTaskData>(TTaskData data, BackGroundTaskPriority priority = BackGroundTaskPriority.Medium, TimeSpan? delay = null, int? maxRetryCountOnError = null, int? retryIntervalSecond = null, CancellationToken cancellationToken = default)
            where TTask : IBackGroundTask<TTaskData>
            where TTaskData : class
        {
            throw new NotImplementedException();
        }

        public Task<string> EnqueueAsync(BackGroundTaskBuilder backGroundTaskBuilder, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
