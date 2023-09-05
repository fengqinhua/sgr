/**************************************************************
 * 
 * 唯一标识：1ffcac2b-4d2f-41cc-8707-1c4baeb8653c
 * 命名空间：Sgr.Security
 * 创建时间：2023/8/28 11:46:24
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.DependencyInjection;
using Sgr.Caching.Services;
using Sgr.UPMS;
using Sgr.UPMS.Domain.Roles;
using Sgr.UPMS.Domain.Users;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sgr.Security
{
    public class UpmsFunctionPermissionGrantingService : IFunctionPermissionGrantingService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICacheManager _cacheManager;


        public UpmsFunctionPermissionGrantingService(IServiceProvider serviceProvider, ICacheManager cacheManager)
        {
            _serviceProvider = serviceProvider;
            _cacheManager = cacheManager;
        }


        private IUserRepository GetUserRepository()
        {
            return _serviceProvider.GetRequiredService<IUserRepository>();
        }

        private IRoleRepository GetRoleRepository()
        {
            return _serviceProvider.GetRequiredService<IRoleRepository>();
        }


        public virtual async Task<bool> IsGrantedAsync(string userId, string functionPermissionName)
        {
            if (!long.TryParse(userId, out long id))
                return false;

            //获取用户
            User? user = await _cacheManager.Get(string.Format(CacheKeys.USER_KEY_WITH_ROLES, userId), async () =>
            {
                return await GetUserRepository().GetAsync(id, new string[] { "Roles" });
            });

            if (user == null)
                return false;
            if(user.IsSuperAdmin)
                return true;

            //获取角色
            bool result = false;
            foreach (UserRole item in user.Roles)
            {
                Role? role = await _cacheManager.Get(string.Format(CacheKeys.ROLE_KEY_WITH_RESOURCES, item.RoleId), async () =>
                {
                    return await GetRoleRepository().GetAsync(item.RoleId, new string[] { "Resources" });
                });

                if (role == null)
                    break;

                if(role.AnyPermission(ResourceType.FunctionalPermission, functionPermissionName))
                {
                    result = true;
                    break;
                }
            }

            return result;
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