/**************************************************************
 * 
 * 唯一标识：207f5840-23b3-41b7-9558-b460af551664
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/24 17:24:28
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
    /// 注销组织
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，当决定不再使用平台时，希望可将组织从当前平台注销
    /// </remarks>
    public class CancellationOrgCommand : IRequest<bool>
    {
        /// <summary>
        /// 组织机构标识
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 当前账号密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
