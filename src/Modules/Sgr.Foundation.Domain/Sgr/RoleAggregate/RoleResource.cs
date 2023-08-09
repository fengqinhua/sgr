/**************************************************************
 * 
 * 唯一标识：6dec421a-123e-4fc5-9328-4ed3a20960eb
 * 命名空间：Sgr.RoleAggregate
 * 创建时间：2023/8/7 17:46:07
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.RoleAggregate
{
    /// <summary>
    /// 角色资源
    /// </summary>
    public class RoleResource : CreationAuditedEntity<long, long>
    {
        /// <summary>
        /// 角色资源
        /// </summary>
        protected RoleResource() { }

        /// <summary>
        /// 角色资源
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceCode"></param>
        public RoleResource(ResourceType resourceType, string resourceCode)
            : this()
        {
            this.ResourceType = resourceType;
            this.ResourceCode = resourceCode;
        }

        /// <summary>
        /// 资源标识
        /// </summary>
        public string ResourceCode { get; private set; } = "";
        /// <summary>
        /// 资源类型
        /// </summary>
        public ResourceType ResourceType { get; private set; } = ResourceType.Undefined;
    }

    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// 功能权限
        /// </summary>
        FunctionalPermission,
        /// <summary>
        /// 数据权限
        /// </summary>
        DataPermission
    }
}
