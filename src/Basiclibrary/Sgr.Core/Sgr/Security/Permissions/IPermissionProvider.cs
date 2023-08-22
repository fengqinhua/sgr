/**************************************************************
 * 
 * 唯一标识：a9710a8f-321d-4d84-be3e-bcf414d4ac36
 * 命名空间：Sgr.Security.Permissions
 * 创建时间：2023/8/22 11:04:54
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

namespace Sgr.Security.Permissions
{
    public interface IPermissionProvider
    {
        /// <summary>
        /// 获取功能权限列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<FunctionPermission>> GetFunctionPermissionsAsync();
    }
}
