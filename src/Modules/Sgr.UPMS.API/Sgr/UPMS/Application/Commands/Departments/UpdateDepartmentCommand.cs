/**************************************************************
 * 
 * 唯一标识：088cbf60-3525-4de5-a7b1-8f5392573c64
 * 命名空间：Sgr.UPMS.Application.Commands.Departments
 * 创建时间：2023/8/23 18:09:33
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;

namespace Sgr.UPMS.Application.Commands.Departments
{
    /// <summary>
    /// 修改部门信息
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望可以修改某一个已存在的部门的基本信息
    /// </remarks>
    public class UpdateDepartmentCommand : IRequest<bool>
    {
        /// <summary>
        /// 部门标识
        /// </summary>
        public long DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }

        /// <summary>
        /// 负责人/联系人
        /// </summary>
        public string? Leader { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string? Email { get; set; }

    }
}
