/**************************************************************
 * 
 * 唯一标识：f7a9d210-3eaf-4a55-99e8-45fabef81954
 * 命名空间：Sgr.DepartmentAggregate
 * 创建时间：2023/8/6 11:56:33
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;
using Sgr.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.DepartmentAggregate
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department : CreationAndModifyAuditedEntity<long, long>, IAggregateRoot, IOptimisticLock, IMustHaveOrg<long>, ITreeNode<long>
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        public string Code { get; private set; } = string.Empty;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;
        /// <summary>
        /// 部门状态
        /// </summary>
        public EntityStates State { get; internal protected set; } = EntityStates.Normal;

        #region  ITreeNode (树形结构)

        /// <summary>
        /// 上级组织Id
        /// </summary>
        public long ParentId { get; internal protected set; } = 0;
        /// <summary>
        /// 树节点层次目录
        /// </summary>
        public string NodePath { get; internal protected set; } = string.Empty;

        #endregion

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
