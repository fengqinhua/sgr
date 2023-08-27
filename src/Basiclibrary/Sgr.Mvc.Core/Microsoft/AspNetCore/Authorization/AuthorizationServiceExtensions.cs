/**************************************************************
 * 
 * 唯一标识：55f86bea-a549-4df7-8a60-c238ee229165
 * 命名空间：Microsoft.AspNetCore.Authorization
 * 创建时间：2023/8/26 21:51:38
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System.Security.Claims;
using System.Security;
using System.Threading.Tasks;
using Sgr.Security.Permissions;
using Sgr.Security;

namespace Microsoft.AspNetCore.Authorization
{
    public static class AuthorizationServiceExtensions
    {
        public static Task<bool> AuthorizeAsync(this IAuthorizationService service, ClaimsPrincipal user, FunctionPermission permission)
        {
            return AuthorizeAsync(service, user, permission, null);
        }

        public static async Task<bool> AuthorizeAsync(this IAuthorizationService service, ClaimsPrincipal user, FunctionPermission permission, object? resource)
        {
            if (user == null)
                return false;

            return (await service.AuthorizeAsync(user, resource, new FunctionPermissionRequirement(permission))).Succeeded;
        }
    }
}
