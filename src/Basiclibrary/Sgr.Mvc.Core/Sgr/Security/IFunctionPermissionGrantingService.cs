/**************************************************************
 * 
 * 唯一标识：099c57dd-8eba-463e-a2bd-cbe7794bffd8
 * 命名空间：Sgr.Security
 * 创建时间：2023/8/26 21:41:30
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Security.Permissions;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sgr.Security
{
    public interface IFunctionPermissionGrantingService
    {
        Task<bool> IsGrantedAsync(string userId, string functionPermissionName);
        Task<bool> IsGrantedAsync(string userId, FunctionPermissionRequirement requirement);
        Task<bool> IsGrantedAsync(ICurrentUser currentUser, FunctionPermissionRequirement requirement);
        Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, FunctionPermissionRequirement requirement);
    }
}
