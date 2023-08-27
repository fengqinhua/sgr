/**************************************************************
 * 
 * 唯一标识：690ebaa2-7cae-44c4-9661-d1025fb736c6
 * 命名空间：Sgr.UPMS.Application.Commands.Roles
 * 创建时间：2023/8/23 17:44:12
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.Domain.Entities;

namespace Sgr.UPMS.Application.Commands.Roles
{
    /// <summary>
    /// 调整账号状态
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为系统管理员，希望可以调整某一个已存在的账号的可用状态
    /// </remarks>
    public class ModifyUserStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// 账号标识
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 可用状态
        /// </summary>
        public EntityStates State { get;  set; } = EntityStates.Normal;
    }
}
