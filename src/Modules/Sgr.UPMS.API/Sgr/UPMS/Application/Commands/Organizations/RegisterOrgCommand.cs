/**************************************************************
 * 
 * 唯一标识：5fedcfbc-f581-4db3-9ebe-5ef0eb23ae9b
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/23 13:52:11
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.UPMS.Application.Commands.Organizations
{
    /// <summary>
    /// 创建组织机构
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望可以在平台上快速完成组织机构注册  
    /// </remarks>
    public class RegisterOrgCommand
    {
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 人员规模编码
        /// </summary>
        public string StaffSizeCode { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Remarks { get; set; }

        /// <summary>
        /// 组织机构管理员账号名称
        /// </summary>
        public string AdminName { get; set; } = string.Empty;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// 组织机构管理员账号密码
        /// </summary>
        public string AdminPassword { get; set; } = string.Empty;
    }
}
