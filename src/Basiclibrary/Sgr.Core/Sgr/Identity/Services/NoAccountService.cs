/**************************************************************
 * 
 * 唯一标识：cbeb0fb1-ec37-4765-a5f2-fa5cc04130a2
 * 命名空间：Sgr.Identity.Services
 * 创建时间：2023/8/21 18:10:57
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
    public class NoAccountService : IAccountService
    {
        public Task CreateLoginLog(string loginName, string ipAddress, string loginWay, string clientBrowser, string clientOs, bool status, string remark, string orgId)
        {
            return Task.CompletedTask;
        }

        public Task CreateRefreshTokenAsync(string userId, string refreshToken, int minutes = 720)
        {
            return Task.CompletedTask;
        }

        public Task<Tuple<AccountLoginResults, Account?>> ValidateAccountAsync(string loginName, string password)
        {
            return Task.FromResult(new Tuple<AccountLoginResults, Account?>(AccountLoginResults.NotExist, null));
        }

        public Task<Tuple<ValidateRefreshTokenResults, Account?>> ValidateRefreshTokenAsync(string userId, string refreshToken)
        {
            return Task.FromResult(new Tuple<ValidateRefreshTokenResults, Account?>(ValidateRefreshTokenResults.Expire, null));
        }
    }
}
