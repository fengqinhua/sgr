/**************************************************************
 * 
 * 唯一标识：7e65e029-1c97-43d4-b63e-d216ac6f7bd2
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/23 18:10:42
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.UPMS.Application.Commands.Users
{
    /// <summary>
    /// 删除账号
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望可以删除某一个已存在的账号
    /// </remarks>
    public class DeleteUserCommand
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 是否级联删除
        /// </summary>
        public bool IsCascade { get; set; }
    }
}
