/**************************************************************
 * 
 * 唯一标识：10607109-6a4a-4e1c-b746-4856303408ff
 * 命名空间：Sgr.UPMS.Application.Commands.Duties
 * 创建时间：2023/8/23 19:20:13
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.Domain.Entities;

namespace Sgr.UPMS.Application.Commands.Duties
{
    /// <summary>
    /// 调整职务状态
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望可以调整某一个已存在的职务的可用状态
    /// </remarks>
    public class ModifyDutyStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// 职务标识
        /// </summary>
        public long DutyId { get; set; }

        /// <summary>
        /// 可用状态
        /// </summary>
        public EntityStates State { get; set; } = EntityStates.Normal;
    }
}
