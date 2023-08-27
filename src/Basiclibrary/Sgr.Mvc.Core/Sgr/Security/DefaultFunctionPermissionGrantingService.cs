﻿/**************************************************************
 * 
 * 唯一标识：1d2ed368-21ce-45c2-93e9-a67224f7a9ba
 * 命名空间：Sgr.Security
 * 创建时间：2023/8/26 21:47:35
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Identity.Services;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security;
using System.Linq;
using Sgr.Identity;

namespace Sgr.Security
{
    public class DefaultFunctionPermissionGrantingService : IFunctionPermissionGrantingService
    {
        private readonly IRoleService _roleService;
        private readonly IAccountService _accountService;

        public DefaultFunctionPermissionGrantingService(IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }

        public async Task<bool> IsGrantedAsync(string userId, string functionPermissionName)
        {
            if (string.IsNullOrEmpty(functionPermissionName))
                return false;

            AccountRoles accountRoleIds = await _accountService.GetAccountRoleIdsAsync(userId);
            
            if (accountRoleIds.IsAdmin)
                return true;

            foreach (string accountRoleId in accountRoleIds.RoleIds)
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

        public Task<bool> IsGrantedAsync(string userId, FunctionPermissionRequirement requirement)
        {
            return IsGrantedAsync(userId, requirement.Permission.Name);
        }

        public Task<bool> IsGrantedAsync(ICurrentUser currentUser, FunctionPermissionRequirement requirement)
        {
            return IsGrantedAsync(currentUser.Id, requirement);
        }

        public async Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, FunctionPermissionRequirement requirement)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(f => f.ValueType == Constant.CLAIM_USER_ID);
            if (claim == null)
                return false;
            return await IsGrantedAsync(claim.Value, requirement);
        }
    }
}