/**************************************************************
 * 
 * 唯一标识：8d70f72d-b35f-4566-8a93-c19aea175e17
 * 命名空间：Sgr.Identity.Abstractions.Sgr.Identity
 * 创建时间：2023/8/21 16:14:58
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Identity
{
    public enum AccountLoginResults
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 账号不存在
        /// </summary>
        NotExist = 1,
        /// <summary>
        /// 账号或密码错误
        /// </summary>
        WrongPassword = 2,
        /// <summary>
        /// 账号被禁用
        /// </summary>
        IsDeactivate = 3
    }
}
