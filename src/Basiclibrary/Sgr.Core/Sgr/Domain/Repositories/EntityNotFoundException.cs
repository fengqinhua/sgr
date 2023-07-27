/**************************************************************
 * 
 * 唯一标识：19eea4ac-97f7-4af2-b5e8-738ac766685f
 * 命名空间：Sgr.Domain.Repositories
 * 创建时间：2023/7/27 10:28:48
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Domain.Repositories
{
    /// <summary>
    /// 异常类：未发现实体对象
    /// </summary>
    public class EntityNotFoundException : BusinessException
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        public Type EntityType { get; set; }
        /// <summary>
        /// 实体ID
        /// </summary>
        public object? Id { get; set; }

        /// <summary>
        /// 异常类：未发现实体对象
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="id"></param>
        public EntityNotFoundException(Type entityType,object id)
            : base(id == null ? $"未发现给定Id的实体对象，实体类型：{entityType.FullName}" : $"未发现给定Id的实体对象，实体类型：{entityType.FullName}，Id：{id}")
        {
            EntityType = entityType;
            Id = id;
        }

        /// <summary>
        /// 异常类：未发现实体对象
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="id"></param>
        /// <param name="innerException"></param>
        public EntityNotFoundException(Type entityType,
            object id,
            Exception innerException)
            : base(id == null ? $"未发现给定Id的实体对象，实体类型：{entityType.FullName}" : $"未发现给定Id的实体对象，实体类型：{entityType.FullName}，Id：{id}", innerException)
        {
            EntityType = entityType;
            Id = id;
        }
    }
}
