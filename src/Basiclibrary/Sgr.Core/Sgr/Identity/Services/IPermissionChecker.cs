/**************************************************************
 * 
 * 唯一标识：c086b1fb-6142-416f-986a-bab69241c158
 * 命名空间：Sgr.Identity.Services
 * 创建时间：2023/8/22 11:44:44
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Security.Permissions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sgr.Identity.Services
{
    public interface IPermissionChecker
    {
        Task<bool> IsGrantedAsync(string userId, string functionPermissionName);
        Task<bool> IsGrantedAsync(string userId, FunctionPermission permission);
        Task<bool> IsGrantedAsync(ICurrentUser currentUser, FunctionPermission permission);
        Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, FunctionPermission permission);
    }
}
