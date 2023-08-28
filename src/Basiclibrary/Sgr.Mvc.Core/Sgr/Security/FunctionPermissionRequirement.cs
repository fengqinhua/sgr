/**************************************************************
 * 
 * 唯一标识：0e3496a5-0dcd-4c0b-a20d-8c65d3adbb75
 * 命名空间：Sgr.Security
 * 创建时间：2023/8/26 21:36:26
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Authorization;
using System.Security;
using System;
using Sgr.Security.Permissions;

namespace Sgr.Security
{
    public class FunctionPermissionRequirement : IAuthorizationRequirement
    {
        public FunctionPermissionRequirement(FunctionPermission permission)
        {
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        }

        public FunctionPermission Permission { get; set; }
    }
}
