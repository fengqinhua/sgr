/**************************************************************
 * 
 * 唯一标识：ff008e3f-e283-4b8c-bc2f-551743ec90d9
 * 命名空间：Sgr.Identity.Services
 * 创建时间：2023/8/22 14:55:13
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Identity.Services
{
    public class NoRoleService : IRoleService
    {
        public Task<IEnumerable<string>> GetDataPermissionNamesByAccountRoleIdAsync(string roleId)
        {
            IEnumerable<string> result = new List<string>();
            return Task.FromResult(result);
        }

        public Task<IEnumerable<string>> GetFunctionalPermissionNamesByAccountRoleIdAsync(string roleId)
        {
            IEnumerable<string> result = new List<string>();
            return Task.FromResult(result);
        }
    }
}
