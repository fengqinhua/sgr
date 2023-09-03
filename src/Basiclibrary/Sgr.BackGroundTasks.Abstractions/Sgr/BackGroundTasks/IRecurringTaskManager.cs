/**************************************************************
 * 
 * 唯一标识：92c3be48-b311-4005-856a-2b0a0fc33cb2
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/2 16:04:03
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    /// <summary>
    /// 周期性任务管理器
    /// </summary>
    public interface IRecurringTaskManager
    {
        /// <summary>
        /// 重启任务（如果任务已存在，那么会先停止再重新启动）
        /// </summary>
        /// <param name="recurringTaskId"></param>
        /// <param name="taskData"></param>
        /// <param name="cronExpressions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ReStartAsync(string recurringTaskId,
            object? taskData = null,
            string? cronExpressions = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="recurringTaskId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StopAsync(string recurringTaskId,
            CancellationToken cancellationToken = default);
    }
}
