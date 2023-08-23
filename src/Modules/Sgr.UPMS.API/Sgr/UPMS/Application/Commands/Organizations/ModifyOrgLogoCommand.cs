/**************************************************************
 * 
 * 唯一标识：d175eaca-9d9e-475b-ace5-1a64abf2365a
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/23 15:11:20
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.UPMS.Application.Commands.Organizations
{
    /// <summary>
    /// 修改组织机构Logo
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望可上传或修改组织机构Logo
    /// </remarks>
    public class ModifyOrgLogoCommand
    {
        /// <summary>
        /// 组织机构标识
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 组织机构Logo
        /// </summary>
        public string[]? Logo { get; set; }
    }
}
