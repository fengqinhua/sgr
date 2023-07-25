/**************************************************************
 * 
 * 唯一标识：6f3959c7-0176-428b-bfd6-fc2d8291761b
 * 命名空间：Sgr
 * 创建时间：2023/7/20 21:49:47
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
    /// 当前用户接口的默认实现
    /// </summary>
    public class DefaultCurrentUser : ICurrentUser
    {
        /// <summary>
        /// 当前用户标识
        /// </summary>
        public long Id => Constant.DEFAULT_USERID;

        /// <summary>
        /// 当前用户名称
        /// </summary>
        public string Name => "default";

        /// <summary>
        /// 当前用户所在组织
        /// </summary>
        public long OrgId => Constant.DEFAULT_ORGID;
    }
}
