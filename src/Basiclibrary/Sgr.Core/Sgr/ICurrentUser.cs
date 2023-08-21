/**************************************************************
 * 
 * 唯一标识：a5ec62fc-9e77-4f3b-a5ce-59dc84930b9f
 * 命名空间：Sgr
 * 创建时间：2023/7/20 21:46:54
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr
{
    /// <summary>
    /// 当前用户信息
    /// </summary>
    public interface ICurrentUser
    {
        /// <summary>
        /// 当前用户标识
        /// </summary>
        string Id { get; }
        /// <summary>
        /// 当前用户名称
        /// </summary>
        string LoginName { get; }
        /// <summary>
        /// 当前用户所在组织
        /// </summary>
        string OrgId { get; }
    }
}
