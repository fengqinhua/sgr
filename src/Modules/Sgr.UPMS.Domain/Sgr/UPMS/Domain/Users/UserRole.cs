/**************************************************************
 * 
 * 唯一标识：dfc9c151-681f-4c5a-a144-36e989c972c3
 * 命名空间：Sgr.UserAggregate
 * 创建时间：2023/8/8 9:04:10
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;

namespace Sgr.UPMS.Domain.Users
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRole : EntityBase
    {
        /// <summary>
        /// 用户角色
        /// </summary>
        protected UserRole() { }
        /// <summary>
        /// 用户角色
        /// </summary>
        /// <param name="roleId"></param>
        public UserRole(long roleId) : this() { RoleId = roleId; }
        /// <summary>
        /// 角色标识
        /// </summary>
        public long RoleId { get; set; }
    }
}
