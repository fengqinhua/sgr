/**************************************************************
 * 
 * 唯一标识：7c880758-c717-4705-af80-a03d2de3eab5
 * 命名空间：Sgr.Identity.Abstractions.Sgr.Identity
 * 创建时间：2023/8/21 15:50:57
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Identity.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// 执行登录（验证账号和密码是否正确）
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Tuple<AccountLoginResults, Account?>> ValidateAccountAsync(string loginName, string password);
        /// <summary>
        /// 创建刷新Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken">刷新令牌</param>
        /// <param name="minutes">刷新令牌有效时长（分钟）,默认12小时</param>
        Task CreateRefreshTokenAsync(string userId, string refreshToken, int minutes = 720);
        /// <summary>
        /// 验证刷新Token是否有效
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="refreshToken">刷新令牌</param>
        /// <returns></returns>
        Task<Tuple<ValidateRefreshTokenResults, Account?>> ValidateRefreshTokenAsync(string userId, string refreshToken);

        /// <summary>
        /// 创建登录日志
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="ipAddress"></param>
        /// <param name="loginWay"></param>
        /// <param name="clientBrowser"></param>
        /// <param name="clientOs"></param>
        /// <param name="status"></param>
        /// <param name="remark"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task CreateLoginLog(string loginName, string ipAddress, string loginWay, string clientBrowser, string clientOs, bool status, string remark, string orgId);
    }
}
