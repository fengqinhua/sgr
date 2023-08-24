/**************************************************************
 * 
 * 唯一标识：792578c8-8690-4ed3-a110-92bac8347a8b
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/23 15:06:08
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
    /// 组织机构认证
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望通过企业认证已使用诸如：发票申请等平台更多高级功能
    /// </remarks>
    public class AuthenticationCommand : IRequest<bool>
    {
        /// <summary>
        /// 组织机构标识
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        public string UsciCode { get; set; } = string.Empty;

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
        /// 营业执照
        /// </summary>
        public string[]? BusinessLicense { get; set; }

        
    }
}
