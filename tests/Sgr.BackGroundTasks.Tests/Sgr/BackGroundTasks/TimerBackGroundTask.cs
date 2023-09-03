/**************************************************************
 * 
 * 唯一标识：5863988f-094b-4c62-8f04-707176e71e44
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/3 16:57:06
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    internal class TimerBackGroundTask : IBackGroundTask
    {
        public static DateTimeOffset DateTimeValue = DateTimeOffset.MinValue;

        private readonly ILogger<TimerBackGroundTask> _logger;

        public TimerBackGroundTask(ILogger<TimerBackGroundTask> logger)
        {
            _logger = logger;
        }

        public Task ExecuteAsync()
        {
            DateTimeValue = DateTimeOffset.UtcNow;

            _logger.LogWarning($"TimerBackGroundTask Update DateTimeValue:{DateTimeValue}");

            return Task.CompletedTask;
        }
    }
}
