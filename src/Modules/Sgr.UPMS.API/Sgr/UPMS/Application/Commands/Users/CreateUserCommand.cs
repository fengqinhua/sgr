/**************************************************************
 * 
 * 唯一标识：9af1ba1f-b349-4ed7-9b94-9a36925b7e3e
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/23 18:07:31
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.UPMS.Application.Commands.Users
{
    /// <summary>
    /// 创建账号
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望可以按名称创建一个账号
    /// </remarks>
    public class CreateUserCommand
    {
        /// <summary>
        /// 登录名称
        /// </summary>
        public string LoginName { get; set; } = string.Empty;
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPassword { get; set; } = string.Empty;

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string? UserName { get; set; } = string.Empty;
        /// <summary>
        /// 所属部门Id
        /// </summary>
        public long? DepartmentId { get;  set; }
        /// <summary>
        /// 所属岗位
        /// </summary>
        public long[]? DutyIds { get; set; }
        /// <summary>
        /// 所属角色
        /// </summary>
        public long[]? RoleIds { get; set; }


        /// <summary>
        /// 组织标识
        /// </summary>
        public long OrgId { get; set; }
    }
}
