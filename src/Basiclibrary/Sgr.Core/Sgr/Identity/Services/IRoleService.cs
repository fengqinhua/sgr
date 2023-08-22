/**************************************************************
 * 
 * 唯一标识：fd1019af-043b-44e9-927f-3a448ed2b823
 * 命名空间：Sgr.Identity.Services
 * 创建时间：2023/8/22 12:54:44
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Identity.Services
{
    public interface IRoleService
    {

        /// <summary>
        /// 获取角色关联的功能权限名称列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetFunctionalPermissionNamesByAccountRoleIdAsync(string roleId);
        /// <summary>
        /// 获取角色关联的数据权限名称列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetDataPermissionNamesByAccountRoleIdAsync(string roleId);
    }
}
