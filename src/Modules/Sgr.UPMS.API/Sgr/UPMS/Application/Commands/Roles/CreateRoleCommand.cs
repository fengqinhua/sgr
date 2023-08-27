/**************************************************************
 * 
 * 唯一标识：99ef014d-a495-448b-8675-4cdbedd90897
 * 命名空间：Sgr.UPMS.Application.Commands.Roles
 * 创建时间：2023/8/23 17:28:10
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
    /// 创建角色
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为系统管理员，希望可以按名称创建一个角色 
    /// </remarks>
    public class CreateRoleCommand : IRequest<bool>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }

        /// <summary>
        /// 组织标识
        /// </summary>
        public long? OrgId { get; set; }
    }
}
