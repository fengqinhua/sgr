/**************************************************************
 * 
 * 唯一标识：23264587-9d9a-4d47-a57e-c9ff8f9ee445
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 11:34:22
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.ComponentModel;
using System.Linq;

namespace Sgr.BackGroundTasks
{
    /// <summary>
    /// 后台任务的元数据信息
    /// </summary>
    public class BackGroundTaskMetadata
    {
        /// <summary>
        /// 后台任务类的类型
        /// </summary>
        public Type TaskClassType { get; private set; }
        /// <summary>
        /// 后台任务输入参数类型
        /// </summary>
        public Type? TaskDataType { get; private set; }
        /// <summary>
        /// 后台任务名称
        /// </summary>
        public string TaskName { get; private set; }
        /// <summary>
        /// 后台任务是否存在入参
        /// </summary>
        public bool HasInputParameter 
        { 
            get 
            { 
                return TaskDataType != null; 
            } 
        }


        public BackGroundTaskMetadata(Type taskClassType)
        {
            TaskClassType = taskClassType;
            TaskDataType = getTaskDataType(taskClassType);
            TaskName = taskClassType.GetCustomAttributes(true)
                .OfType<DisplayNameAttribute>()
                .FirstOrDefault()?.DisplayName ?? TaskClassType.FullName ?? Guid.NewGuid().ToString("N");
        }

        private Type? getTaskDataType(Type taskClassType)
        {
            if (typeof(IBackGroundTask).IsAssignableFrom(taskClassType))
                return null;

            foreach (Type item in taskClassType.GetInterfaces())
            {
                if (!item.IsGenericType
                    || item.GetGenericTypeDefinition() != typeof(IBackGroundTask<>))
                    continue;

                var genericArguments = item.GetGenericArguments();
                if (genericArguments.Length == 1)
                    return genericArguments[0];
            }

            throw new BackGroundTaskException($"TaskClassType ({taskClassType.FullName}) 未实现 IBackGroundTask<TData> 接口");
        }
    }
}
