/**************************************************************
 * 
 * 唯一标识：088cbf60-3525-4de5-a7b1-8f5392573c64
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/23 18:09:33
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.UPMS.Application.Commands.Users
{
    /// <summary>
    /// 修改账号信息
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望可以修改某一个已存在账号的基本信息
    /// </remarks>
    public class UpdateUserCommand
    {
        /// <summary>
        /// 部门标识
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string? UserName { get; set; } = string.Empty;
        /// <summary>
        /// 所属部门Id
        /// </summary>
        public long? DepartmentId { get; set; }
        /// <summary>
        /// 所属岗位
        /// </summary>
        public long[]? DutyIds { get; set; }
        /// <summary>
        /// 所属角色
        /// </summary>
        public long[]? RoleIds { get; set; }

    }
}
