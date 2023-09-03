/**************************************************************
 * 
 * 唯一标识：7e63d79b-d354-4584-9b0d-e54d08a80f2b
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 15:43:48
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;

namespace Sgr.BackGroundTasks
{
    public class BackGroundTaskContext
    {
        public IServiceProvider ServiceProvider { get; }

        public Type TaskClassType { get; }

        public object? TaskData { get; }

        public BackGroundTaskContext(IServiceProvider serviceProvider, Type backGroundTaskContext, object? taskData = null)
        {
            ServiceProvider = serviceProvider;

            TaskClassType = backGroundTaskContext;
            TaskData = taskData;
        }
    }
}
