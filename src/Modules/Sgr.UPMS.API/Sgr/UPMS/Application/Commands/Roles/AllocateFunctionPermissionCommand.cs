/**************************************************************
 * 
 * 唯一标识：243f6331-a664-4f26-bcbe-db209cdbe780
 * 命名空间：Sgr.UPMS.Application.Commands.Roles
 * 创建时间：2023/8/23 17:31:40
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Roles;
using System.Collections;
using System.Collections.Generic;

namespace Sgr.UPMS.Application.Commands.Roles
{
    /// <summary>
    /// 为角色授予功能权限
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为系统管理员，希望可以为角色分配相应的功能权限
    /// </remarks>
    public class AllocateFunctionPermissionCommand : IRequest<bool>
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        public long RoleId { get; set; }    
        /// <summary>
        /// 功能资源列表
        /// </summary>
        public string[]? FunctionPermissions { get; set; }
    }
}
