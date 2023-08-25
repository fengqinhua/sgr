/**************************************************************
 * 
 * 唯一标识：14882f14-2d6d-4b61-a82f-5e333cdcd9f8
 * 命名空间：Sgr.Identity.Services
 * 创建时间：2023/8/22 11:46:58
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Security.Permissions;
using System.Threading.Tasks;
using Sgr.Identity.Services;
using System.Security;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using System.Linq;

namespace Sgr.Identity.Services
{
    public class PermissionChecker : IPermissionChecker
    {
        private readonly IRoleService _roleService;
        private readonly IAccountService _accountService;

        public PermissionChecker(IAccountService accountService,IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        public async Task<bool> IsGrantedAsync(string userId, string functionPermissionName)
        {
            if (string.IsNullOrEmpty(functionPermissionName))
                return false;

            IEnumerable<string> accountRoleIds = await _accountService.GetAccountRoleIdsAsync(userId);
            foreach (var accountRoleId in accountRoleIds)
            {
                IEnumerable<string> permissions = await _roleService.GetFunctionalPermissionNamesByAccountRoleIdAsync(accountRoleId);
                foreach (var permission in permissions)
                {
                    if (functionPermissionName.Equals(permission, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                }
            }

            return false;
        }

        public Task<bool> IsGrantedAsync(string userId, FunctionPermission permission)
        {
            return IsGrantedAsync(userId, permission.Name);
        }

        public Task<bool> IsGrantedAsync(ICurrentUser currentUser, FunctionPermission permission)
        {
            return IsGrantedAsync(currentUser.Id, permission);
        }

        public async Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, FunctionPermission permission)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(f => f.ValueType == Constant.CLAIM_USER_ID);
            if (claim == null)
                return false;
            return await IsGrantedAsync(claim.Value, permission);
        }

    }
}
