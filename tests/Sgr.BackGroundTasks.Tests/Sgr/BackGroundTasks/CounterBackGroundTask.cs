/**************************************************************
 * 
 * 唯一标识：87834609-08a5-482a-81bd-050b80c7505e
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/3 13:37:14
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    internal class CounterBackGroundTask : IBackGroundTask
    {
        public static int Index = 0;

        public Task ExecuteAsync()
        {
            Index++;
            return Task.CompletedTask;
        }
    }
}
