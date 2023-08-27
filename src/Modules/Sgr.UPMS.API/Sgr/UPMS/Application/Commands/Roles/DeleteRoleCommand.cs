/**************************************************************
 * 
 * 唯一标识：d6c39bbd-fa18-4ed7-8402-2c9c085b8058
 * 命名空间：Sgr.UPMS.Application.Commands.Roles
 * 创建时间：2023/8/23 17:42:51
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;

namespace Sgr.UPMS.Application.Commands.Roles
{
    /// <summary>
    /// 删除角色
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为系统管理员，希望可以删除某一个已存在的角色
    /// </remarks>
    public class DeleteRoleCommand : IRequest<bool>
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        public long RoleId { get; set; }
    }
}
