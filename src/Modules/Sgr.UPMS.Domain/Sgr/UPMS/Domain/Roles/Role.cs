﻿/**************************************************************
 * 
 * 唯一标识：36b8fda9-6066-4cfd-8ff9-c68c77a9e30c
 * 命名空间：Sgr.RoleAggregate
 * 创建时间：2023/8/7 15:55:17
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities.Auditing;
using Sgr.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Sgr.Exceptions;

namespace Sgr.UPMS.Domain.Roles
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : CreationAndModifyAuditedEntity<long, long>, IAggregateRoot, IOptimisticLock, IMustHaveOrg<long>, IHaveCode
    {
        private List<RoleResource> _resources;
        /// <summary>
        /// 资源清单
        /// </summary>
        public IEnumerable<RoleResource> Resources => _resources.AsReadOnly();

        /// <summary>
        /// 角色
        /// </summary>
        protected Role()
        {
            _resources = new List<RoleResource>();
            State = EntityStates.Normal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="roleName"></param>
        /// <param name="orgId"></param>
        /// <param name="orderNumber"></param>
        /// <param name="remarks"></param>
        public Role(string code, string roleName, long orgId, int orderNumber = 0, string? remarks = null)
            : this()
        {
            Code = code;
            RoleName = roleName;
            OrderNumber = orderNumber;
            Remarks = remarks;
            OrgId = orgId;
        }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string Code { get; private set; } = string.Empty;
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; } = "";
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EntityStates State { get; internal protected set; } = EntityStates.Normal;


        /// <summary>
        /// 重置指定类型的资源清单
        /// </summary>
        /// <param name="resourceType"></param>
        public void ResetPermission(ResourceType resourceType)
        {
            _resources.RemoveAll(f => f.ResourceType == resourceType);
        }

        /// <summary>
        /// 分配资源
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceCode"></param>
        public void AddPermission(ResourceType resourceType, string resourceCode)
        {
            var roleResource = _resources.FirstOrDefault(f => f.ResourceType == resourceType && f.ResourceCode == resourceCode);
            if (roleResource == null)
            {
                roleResource = new RoleResource(resourceType, resourceCode);
                _resources.Add(roleResource);
            }
        }

        /// <summary>
        /// 是否存在某权限
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceCode"></param>
        /// <returns></returns>
        public bool AnyPermission(ResourceType resourceType, string resourceCode)
        {
            return _resources.Any(f => f.ResourceType == resourceType && f.ResourceCode == resourceCode);
        }

        /// <summary>
        /// 调整实体状态
        /// </summary>
        /// <param name="state"></param>
        /// <exception cref="BusinessException"></exception>
        public void ChangeEntityStates(EntityStates state)
        {
            this.State = state;
        }


        #region IOptimisticLock (乐观锁)

        /// <summary>
        /// 行版本
        /// </summary>
        public long RowVersion { get; set; }

        #endregion

        #region IMustHaveOrg  (如果一个实体实现该接口，那么必须存储实体所在组织的ID.)

        /// <summary>
        /// 所属组织
        /// </summary>
        public long OrgId { get; internal protected set; }

        #endregion
    }
}
