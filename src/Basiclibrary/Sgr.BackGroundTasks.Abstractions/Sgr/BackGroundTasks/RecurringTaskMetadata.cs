/**************************************************************
 * 
 * 唯一标识：50512f6a-ee38-4ddf-916a-124f8938c297
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/3 16:19:22
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.BackGroundTasks
{
    public class RecurringTaskMetadata
    {
        private object? _taskData = null;

        public RecurringTaskMetadata(
            string id, 
            string name, 
            string cronExpressions, 
            BackGroundTaskMetadata taskMetadata, 
            object? taskData = null)
        {
            Id = id;
            Name = name;
            CronExpressions = cronExpressions;
            TaskMetadata = taskMetadata;
            TaskData = taskData;
        }

        /// <summary>
        /// 周期性任务标识
        /// </summary>
        public virtual string Id { get; }
        /// <summary>
        /// 周期性任务名称
        /// </summary>
        public virtual string Name { get; }
        /// <summary>
        /// 周期任务Con表达式
        /// </summary>
        public virtual string? CronExpressions { get; set; }
        /// <summary>
        /// 任务元数据
        /// </summary>
        public virtual BackGroundTaskMetadata TaskMetadata { get; }
        /// <summary>
        /// 任务输入参数
        /// </summary>
        public virtual object? TaskData
        {
            get
            {
                return _taskData;
            }
            set
            {
                if (value == null)
                    return;

                if (TaskMetadata.TaskDataType != null
                    && !TaskMetadata.TaskDataType!.IsAssignableFrom(value.GetType()))
                    throw new BackGroundTaskException($"TaskData类型需为 {TaskMetadata.TaskDataType.FullName} 或其派生类!");

                _taskData = value;
            }
        }
    }
}
