/**************************************************************
 * 
 * 唯一标识：afc9c2ce-fe50-4328-a150-60e9cd6d7efd
 * 命名空间：Sgr.Domain.Entities.Auditing
 * 创建时间：2023/7/26 12:14:48
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
    /// 实体的基类，包含创建用户ID、创建时间、最近一次修改用户ID、最近一次修改时间信息（(字符串)
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    [Serializable]
    public abstract class CreationAndModifyAuditedBase<TUserKey> : CreationAuditedEntityBase<TUserKey>, ICreationAndModifyAudited<TUserKey>
    {
        /// <summary>
        /// 实体最近一次修改的用户ID.
        /// </summary>
        public TUserKey? LastModifierUserId { get; set; }
        /// <summary>
        /// 实体的最近一次的修改时间.
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
    }

    /// <summary>
    ///  实体的基类，包含创建用户ID、创建时间、最近一次修改用户ID、最近一次修改时间信息（值类型）
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TUserKey"></typeparam>
    [Serializable]
    public abstract class CreationAndModifyAuditedEntity<TPrimaryKey, TUserKey> : CreationAuditedEntity<TPrimaryKey, TUserKey>, ICreationAndModifyAudited<TUserKey> where TPrimaryKey : struct
    {
        /// <summary>
        /// 实体最近一次修改的用户ID.
        /// </summary>
        public TUserKey? LastModifierUserId { get; set; }
        /// <summary>
        /// 实体的最近一次的修改时间.
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
    }
}
