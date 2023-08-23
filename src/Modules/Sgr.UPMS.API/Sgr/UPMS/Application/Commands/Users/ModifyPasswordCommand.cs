/**************************************************************
 * 
 * 唯一标识：01866399-3abd-4f35-be06-66b99b9136ff
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/23 18:27:30
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.UPMS.Application.Commands.Users
{
    /// <summary>
    /// 修改账号的密码
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为账号的所有者，希望可以修改账号的密码
    /// </remarks>
    public class ModifyPasswordCommand
    {
        /// <summary>
        /// 部门标识
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; } = string.Empty;
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }
}
