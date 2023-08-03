/**************************************************************
 * 
 * 唯一标识：b5f42c9c-b72d-4091-b224-4cb552e0cbf7
 * 命名空间：Sgr.Domain.Entities.Auditing
 * 创建时间：2023/7/26 11:38:47
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
    /// 如果一个实体实现该接口，那么需创建、修改和删除实体的用户及时间
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public interface IFullAudited<TUserKey> : ICreationAndModifyAudited<TUserKey>, ISoftDelete
    {
        /// <summary>
        /// 删除实体的用户ID
        /// </summary>
        TUserKey? DeleterUserId { get; set; }
        /// <summary>
        /// 实体的删除时间.
        /// </summary>
        DateTimeOffset? DeletionTime { get; set; }
    }
}
