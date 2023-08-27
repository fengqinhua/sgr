/**************************************************************
 * 
 * 唯一标识：b82c8e67-8f5f-4274-863b-21ed75ea5eca
 * 命名空间：Sgr.Security.AuthorizationHandlers
 * 创建时间：2023/8/26 21:39:11
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Sgr.Security.AuthorizationHandlers
{
    public class FunctionPermissionHandler : AuthorizationHandler<FunctionPermissionRequirement>
    {
        private readonly IFunctionPermissionGrantingService _permissionGrantingService;

        public FunctionPermissionHandler(IFunctionPermissionGrantingService permissionGrantingService)
        {
            _permissionGrantingService = permissionGrantingService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, FunctionPermissionRequirement requirement)
        {
            if (context.HasSucceeded || !(context?.User?.Identity?.IsAuthenticated ?? false))
                return;

            if (await _permissionGrantingService.IsGrantedAsync(context.User, requirement))
                context.Succeed(requirement);
        }
    }
}
