/**************************************************************
 * 
 * 唯一标识：1d34cd75-aacf-4eff-8b05-04ebe2179529
 * 命名空间：Sgr.Domain.Entities.Auditing
 * 创建时间：2023/7/26 11:36:12
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
    /// 如果一个实体实现该接口，那么需创建和修改实体的用户及时间
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public interface ICreationAndModifyAudited<TUserKey> : ICreationAudited<TUserKey>
    {
        /// <summary>
        /// 实体最近一次修改的用户ID.
        /// </summary>
        TUserKey? LastModifierUserId { get; set; }
        /// <summary>
        /// 实体的最近一次的修改时间.
        /// </summary>
        DateTime? LastModificationTime { get; set; }
    }
}
