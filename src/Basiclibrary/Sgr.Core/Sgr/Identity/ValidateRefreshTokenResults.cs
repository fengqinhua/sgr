/**************************************************************
 * 
 * 唯一标识：ba1a259c-cd7b-41cd-86dd-a51bd51e4879
 * 命名空间：Sgr.Identity
 * 创建时间：2023/8/21 19:53:53
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Identity
{
    public enum ValidateRefreshTokenResults
    {
        /// <summary>
        /// RefreshToken有效
        /// </summary>
        Success = 0,
        /// <summary>
        /// RefreshToken不存在
        /// </summary>
        NotExist = 1,
        /// <summary>
        /// RefreshToken过期
        /// </summary>
        Expire = 2,
        /// <summary>
        /// 账号异常
        /// </summary>
        AccountAbnormal = 3
    }
}
