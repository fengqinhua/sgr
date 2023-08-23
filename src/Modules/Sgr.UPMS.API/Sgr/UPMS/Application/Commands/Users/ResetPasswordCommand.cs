/**************************************************************
 * 
 * 唯一标识：e0c32bf8-4002-4f7d-885a-4624c963bb58
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/23 18:29:45
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.UPMS.Application.Commands.Users
{
    /// <summary>
    /// 重置密码
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望在某个员忘记密码时帮助其重置密码
    /// </remarks>
    public class ResetPasswordCommand
    {
        /// <summary>
        /// 账号标识
        /// </summary>
        public long UserId { get; set; }
    }
}
