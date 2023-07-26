/**************************************************************
 * 
 * 唯一标识：08969ac2-613b-4fe2-9938-98a8d0bd7a97
 * 命名空间：Sgr.Domain.Entities.Auditing
 * 创建时间：2023/7/26 11:41:42
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
    /// 实体的基类，包括创建该实体的用户ID及创建时间信息(字符串)
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    [Serializable]
    public abstract class CreationAuditedEntityBase<TUserKey> : EntityBase, ICreationAudited<TUserKey>
    {
        /// <summary>
        ///  创建实体的用户ID.
        /// </summary>
        public TUserKey? CreatorUserId { get; set; }
        /// <summary>
        /// 实体的创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }

    /// <summary>
    ///  实体的基类，包括创建该实体的用户ID及创建时间信息（值类型）
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TUserKey"></typeparam>
    [Serializable]
    public abstract class CreationAuditedEntity<TPrimaryKey, TUserKey> : Entity<TPrimaryKey>, ICreationAudited<TUserKey> where TPrimaryKey : struct
    {
        /// <summary>
        ///  创建实体的用户ID.
        /// </summary>
        public TUserKey? CreatorUserId { get; set; }
        /// <summary>
        /// 实体的创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
