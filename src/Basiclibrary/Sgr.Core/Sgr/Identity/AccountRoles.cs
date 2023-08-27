/**************************************************************
 * 
 * 唯一标识：4dd1bf08-ae12-48dc-9ff3-cee564038aa1
 * 命名空间：Sgr.Identity
 * 创建时间：2023/8/27 10:47:25
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
    public class AccountRoles
    {
        public AccountRoles(IEnumerable<string> roleIds, bool isAdmin = false)
        {
            IsAdmin = isAdmin;
            RoleIds = roleIds;
        }

        public bool IsAdmin { get; }
        public IEnumerable<string> RoleIds { get; }
    }
}
