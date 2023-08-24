/**************************************************************
 * 
 * 唯一标识：225defbd-1781-4481-b40f-f655fd93774d
 * 命名空间：Sgr.UPMS.Domain.Users
 * 创建时间：2023/8/24 14:56:20
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
using System.Threading;

namespace Sgr.UPMS.Domain.Users
{
    public interface IUserChecker
    {
        /// <summary>
        /// 账号名称是否唯一
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> LoginNameIsUniqueAsync(string loginName,
            long id = 0,
            CancellationToken cancellationToken = default);
    }
}
