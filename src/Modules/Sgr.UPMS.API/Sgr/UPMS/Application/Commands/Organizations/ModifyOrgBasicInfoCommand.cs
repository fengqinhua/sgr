﻿/**************************************************************
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

namespace Sgr.UPMS.Application.Commands.Organizations
{
    /// <summary>
    /// 修改组织机构基本信息
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，当组织机构名称、描述等基本信息存在漏填或误填时，可对这些信息进行修改
    /// </remarks>
    public class ModifyOrgBasicInfoCommand
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
        /// 描述
        /// </summary>
        public string? Remarks { get; set; }
    }
}
