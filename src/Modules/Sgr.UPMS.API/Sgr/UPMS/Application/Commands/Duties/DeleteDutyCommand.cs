/**************************************************************
 * 
 * 唯一标识：7e65e029-1c97-43d4-b63e-d216ac6f7bd2
 * 命名空间：Sgr.UPMS.Application.Commands.Duties
 * 创建时间：2023/8/23 18:10:42
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;

namespace Sgr.UPMS.Application.Commands.Duties
{
    /// <summary>
    /// 删除职务
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望可以删除某一个已存在的职务
    /// </remarks>
    public class DeleteDutyCommand : IRequest<bool>
    {
        /// <summary>
        /// 职务标识
        /// </summary>
        public long DutyId { get; set; }
    }
}
