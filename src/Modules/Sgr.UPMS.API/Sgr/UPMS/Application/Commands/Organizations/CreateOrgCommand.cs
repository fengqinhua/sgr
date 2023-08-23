/**************************************************************
 * 
 * 唯一标识：1ed9a181-218e-4a83-b0dd-6ab2744f722d
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/23 13:50:10
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 * 
 * 
 **************************************************************/

using MediatR;

namespace Sgr.UPMS.Application.Commands.Organizations
{
    /// <summary>
    /// 创建组织机构
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为系统运维人员，希望可以按照用户提供的纸质材料，帮助用户完成企业创建  
    /// </remarks>
    public class CreateOrgCommand : IRequest<bool>
    {
        /// <summary>
        /// 上级组织机构标识
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 组织机构类型编码
        /// </summary>
        public string OrgTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// 所属行政区划编码（默认情况下，选用邮政编码）
        /// </summary>
        public string AreaCode { get; set; } = string.Empty;
        /// <summary>
        /// 人员规模编码
        /// </summary>
        public string StaffSizeCode { get; set; } = string.Empty;
        /// <summary>
        /// 机构负责人/联系人
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
        /// <summary>
        /// 所在地址
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Remarks { get; set; }
        /// <summary>
        /// 组织机构管理员账号名称
        /// </summary>
        public string AdminName { get; set; } = string.Empty;
        /// <summary>
        /// 组织机构管理员账号密码
        /// </summary>
        public string AdminPassword { get; set; } = string.Empty;
    }
}