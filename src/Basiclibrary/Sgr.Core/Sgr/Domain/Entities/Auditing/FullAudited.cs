/**************************************************************
 * 
 * 唯一标识：0c6cdeba-0c2b-4a86-b996-1a41e1bf1dab
 * 命名空间：Sgr.Domain.Entities.Auditing
 * 创建时间：2023/7/26 12:16:51
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Domain.Entities.Auditing
{
    /// <summary>
    /// 实体的基类，包含创建用户ID、创建时间、最近一次修改用户ID、最近一次修改时间信息、删除用户ID、删除时间、是否已删除等信息(字符串)
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    [Serializable]
    public abstract class FullAuditedBase<TUserKey> : CreationAndModifyAuditedBase<TUserKey>, IFullAudited<TUserKey>
    {
        /// <summary>
        /// 删除实体的用户ID
        /// </summary>
        public TUserKey? DeleterUserId { get; set; }
        /// <summary>
        /// 实体的删除时间.
        /// </summary>
        public DateTimeOffset? DeletionTime { get; set; }
        /// <summary>
        /// 标识实体是否已经被软删除. 
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }

    /// <summary>
    /// 实体的基类，包含创建用户ID、创建时间、最近一次修改用户ID、最近一次修改时间信息、删除用户ID、删除时间、是否已删除等信息（值类型）
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TUserKey"></typeparam>
    [Serializable]
    public abstract class FullAudited<TPrimaryKey, TUserKey> : CreationAndModifyAuditedEntity<TPrimaryKey, TUserKey>, IFullAudited<TUserKey> where TPrimaryKey : struct
    {
        /// <summary>
        /// 删除实体的用户ID
        /// </summary>
        public TUserKey? DeleterUserId { get; set; }
        /// <summary>
        /// 实体的删除时间.
        /// </summary>
        public DateTimeOffset? DeletionTime { get; set; }
        /// <summary>
        /// 标识实体是否已经被软删除. 
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
