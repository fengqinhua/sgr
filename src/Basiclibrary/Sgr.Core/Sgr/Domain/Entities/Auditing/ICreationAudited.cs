/**************************************************************
 * 
 * 唯一标识：b125cf26-002e-4928-ad4e-70cdb73f997a
 * 命名空间：Sgr.Domain.Entities.Auditing
 * 创建时间：2023/7/26 11:30:35
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
    /// 如果一个实体实现该接口，那么需创建实体的用户及时间
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public interface ICreationAudited<TUserKey>
    {
        /// <summary>
        ///  创建实体的用户ID.
        /// </summary>
        TUserKey? CreatorUserId { get; set; }
        /// <summary>
        /// 实体的创建时间
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}
