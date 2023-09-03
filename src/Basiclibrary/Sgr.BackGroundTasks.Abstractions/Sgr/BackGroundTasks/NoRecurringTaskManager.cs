/**************************************************************
 * 
 * 唯一标识：5a9784d3-73ec-417c-a186-f9a1144645df
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/3 7:38:40
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
    public class NoRecurringTaskManager : IRecurringTaskManager
    {
        public Task ReStartAsync(string recurringTaskId, object? taskData = null, string? cronExpressions = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(string recurringTaskId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
