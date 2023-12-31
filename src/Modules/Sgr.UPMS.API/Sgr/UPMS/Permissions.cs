﻿/**************************************************************
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

        public static readonly FunctionPermission CreateUserPermission = new("Sgr.UPMS.CreateUser", "账户管理", "创建账号");
        public static readonly FunctionPermission UpdateUserPermission = new("Sgr.UPMS.UpdateUser", "账户管理", "修改账号");
        public static readonly FunctionPermission DeleteUserPermission = new("Sgr.UPMS.DeleteUser", "账户管理", "删除账号");
        public static readonly FunctionPermission ModifyUserStatusPermission = new("Sgr.UPMS.ModifyUserStatus", "账户管理", "调整状态");
        public static readonly FunctionPermission ResetPasswordPermission = new("Sgr.UPMS.ResetPassword", "账户管理", "重置密码");

        public static readonly FunctionPermission ModifyUserPermission = new("Sgr.UPMS.ModifyUser", "个人中心", "修改信息");
        public static readonly FunctionPermission ModifyPasswordPermission = new("Sgr.UPMS.ModifyPassword", "个人中心", "修改密码");

        public static readonly FunctionPermission CreateRolePermission = new("Sgr.UPMS.CreateRole", "角色管理", "创建角色", true);
        public static readonly FunctionPermission UpdateRolePermission = new("Sgr.UPMS.UpdateRole", "角色管理", "修改角色");
        public static readonly FunctionPermission DeleteRolePermission = new("Sgr.UPMS.DeleteRole", "角色管理", "删除角色", true);
        public static readonly FunctionPermission ModifyRoleStatusPermission = new("Sgr.UPMS.ModifyRoleStatus", "角色管理", "调整状态");
        public static readonly FunctionPermission AllocateFunctionPermissionPermission = new("Sgr.UPMS.AllocateFunctionPermission", "角色管理", "授予功能权限", true);

        public static readonly FunctionPermission CreateDutyPermission = new("Sgr.UPMS.CreateDuty", "职务管理", "创建职务");
        public static readonly FunctionPermission UpdateDutyPermission = new("Sgr.UPMS.UpdateDuty", "职务管理", "修改职务");
        public static readonly FunctionPermission DeleteDutyPermission = new("Sgr.UPMS.DeleteDuty", "职务管理", "删除职务");
        public static readonly FunctionPermission ModifyDutyStatusPermission = new("Sgr.UPMS.ModifyDutyStatus", "职务管理", "调整状态");

        public static readonly FunctionPermission CreateDepartmentPermission = new("Sgr.UPMS.CreateDepartment", "部门管理", "创建部门");
        public static readonly FunctionPermission UpdateDepartmentPermission = new("Sgr.UPMS.UpdateDepartment", "部门管理", "修改部门");
        public static readonly FunctionPermission DeleteDepartmentPermission = new("Sgr.UPMS.DeleteDepartment", "部门管理", "删除部门");


        public Task<IEnumerable<FunctionPermission>> GetFunctionPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                //组织管理
                CreateOrgPermission,
                UpdateOrgPermission,
                CancellationOrgPermission,
                AssociatedParentOrgPermission,
                AuthenticationOrgPermission,
                ModifyOrgLogoPermission,
                //账户管理
                CreateUserPermission,
                UpdateUserPermission,
                DeleteUserPermission,
                ModifyUserStatusPermission,
                ResetPasswordPermission,
                //个人中心
                ModifyUserPermission,
                ModifyPasswordPermission,
                //角色管理
                CreateRolePermission,
                UpdateRolePermission,
                DeleteRolePermission,
                ModifyRoleStatusPermission,
                AllocateFunctionPermissionPermission,
                //职务管理
                CreateDutyPermission,
                UpdateDutyPermission,
                DeleteDutyPermission,
                ModifyDutyStatusPermission,
                //部门管理
                CreateDepartmentPermission,
                UpdateDepartmentPermission,
                DeleteDepartmentPermission
            }
            .AsEnumerable());
        }
    }
}
