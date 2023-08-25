/**************************************************************
 * 
 * 唯一标识：8e5b99f3-41cb-4e1c-a980-a64e78b5df23
 * 命名空间：Sgr.UPMS
 * 创建时间：2023/8/25 16:27:12
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Sgr.UPMS
{
    public class Permissions : IPermissionProvider
    {
        public static readonly FunctionPermission CreateOrgPermission = new("Sgr.UPMS.CreateOrg", "组织机构", "创建组织", true);
        public static readonly FunctionPermission UpdateOrgPermission = new("Sgr.UPMS.UpdateOrg", "组织机构", "修改组织");
        public static readonly FunctionPermission CancellationOrgPermission = new("Sgr.UPMS.CancellationOrg", "组织机构", "注销组织");

        public static readonly FunctionPermission AuthenticationOrgPermission = new("Sgr.UPMS.AuthenticationOrg", "组织机构", "组织机构认证");
        public static readonly FunctionPermission AssociatedParentOrgPermission = new("Sgr.UPMS.AssociatedParentOrg", "组织机构", "绑定上级组织");
        public static readonly FunctionPermission ModifyOrgLogoPermission = new("Sgr.UPMS.ModifyOrgLogo", "组织机构", "上传Logo");



        public Task<IEnumerable<FunctionPermission>> GetFunctionPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                CreateOrgPermission,
                UpdateOrgPermission,
                CancellationOrgPermission,
                AssociatedParentOrgPermission,
                AuthenticationOrgPermission,
                ModifyOrgLogoPermission
            }
            .AsEnumerable());
        }
    }
}
