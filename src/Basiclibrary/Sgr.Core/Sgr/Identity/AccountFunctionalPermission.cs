/**************************************************************
 * 
 * 唯一标识：832e6b18-2d08-46d4-9511-56ba48d5c417
 * 命名空间：Sgr.Identity
 * 创建时间：2023/8/28 10:03:47
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
    public class AccountFunctionalPermission
    {
        public AccountFunctionalPermission(IEnumerable<string> functionalPermissions, bool isAdmin = false)
        {
            IsAdmin = isAdmin;
            FunctionalPermissions = functionalPermissions;
        }

        public bool IsAdmin { get; }
        public IEnumerable<string> FunctionalPermissions { get; }
    }
}
