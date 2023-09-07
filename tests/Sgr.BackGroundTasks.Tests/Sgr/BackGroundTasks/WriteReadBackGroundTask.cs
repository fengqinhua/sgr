/**************************************************************
 * 
 * 唯一标识：0c5f1ca6-440f-446c-8823-adb57216bd5d
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/7 15:49:24
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

    public class WriteReadArgs
    {
        public string Value { get; set; } = string.Empty;
    }

    internal class WriteReadBackGroundTask : IBackGroundTask<WriteReadArgs>
    {
        public static string Value = string.Empty;

        private readonly ILogger<TimerBackGroundTask> _logger;

        public WriteReadBackGroundTask(ILogger<TimerBackGroundTask> logger)
        {
            _logger = logger;
        }

        public Task ExecuteAsync(WriteReadArgs args)
        {
            Value = args.Value;

            _logger.LogWarning($"WriteReadBackGroundTask Update Value:{Value}");

            return Task.CompletedTask;
        }
    }

}
