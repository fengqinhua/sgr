/**************************************************************
 * 
 * 唯一标识：f42efc66-def0-465f-a2ea-f622bb8daf9a
 * 命名空间：Sgr.UPMS.Application.Commands.Roles
 * 创建时间：2023/8/23 17:40:22
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;

namespace Sgr.UPMS.Application.Commands.Roles
{
    /// <summary>
    /// 删除角色
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为系统管理员，希望可以修改某一个已存在的角色的基本信息
    /// </remarks>
    public class UpdateRoleCommand
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        public EntityStates State { get; set; } = EntityStates.Normal;
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
    }
}
