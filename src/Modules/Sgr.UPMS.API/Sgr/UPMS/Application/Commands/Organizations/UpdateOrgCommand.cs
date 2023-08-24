/**************************************************************
 * 
 * 唯一标识：174c07a4-9617-4bab-a4e3-681c05ec5099
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/23 15:03:08
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;

namespace Sgr.UPMS.Application.Commands.Organizations
{
    /// <summary>
    /// 修改组织机构基本信息
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为平台管理员或组织机构管理人员，当组织机构名称、描述等基本信息存在漏填或误填时，可对这些信息进行修改
    /// </remarks>
    public class UpdateOrgCommand : IRequest<bool>
    {
        /// <summary>
        /// 组织机构标识
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 人员规模编码
        /// </summary>
        public string StaffSizeCode { get; set; } = string.Empty;

        /// <summary>
        /// 组织机构类型编码
        /// </summary>
        public string OrgTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// 所属行政区划编码（默认情况下，选用邮政编码）
        /// </summary>
        public string AreaCode { get; set; } = string.Empty;
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
    }
}
