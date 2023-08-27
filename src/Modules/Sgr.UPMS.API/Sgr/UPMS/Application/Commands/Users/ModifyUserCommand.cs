/**************************************************************
 * 
 * 唯一标识：3d52ad87-6324-44ff-a20b-2fb08e4f4280
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/23 18:24:50
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;

namespace Sgr.UPMS.Application.Commands.Users
{
    /// <summary>
    /// 修改账号的人员信息
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为账号的所有者，希望可以修改账号的人员信息
    /// </remarks>
    public class ModifyUserCommand : IRequest<bool>
    {
        /// <summary>
        /// 账号标识
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string? UserName { get; set; } = string.Empty;
        /// <summary>
        /// 用户绑定的手机号码
        /// </summary>
        public string? UserPhone { get; set; }
        /// <summary>
        /// 用户绑定的邮箱地址
        /// </summary>
        public string? UserEmail { get; set; }
        /// <summary>
        /// 用户QQ号码
        /// </summary>
        public string? QQ { get; set; }
        /// <summary>
        /// 用户微信号码
        /// </summary>
        public string? Wechat { get; set; }
    }
}
