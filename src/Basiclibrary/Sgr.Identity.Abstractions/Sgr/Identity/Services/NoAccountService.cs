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
        public Task CreateRefreshTokenAsync(string loginName, string refreshToken, int minutes = 720)
        {
            return Task.CompletedTask;
        }

        public Task<Account?> GetAccountByLoginNameAsync(string loginName)
        {
            return Task.FromResult(default(Account?));
        }

        public Task<Tuple<AccountLoginResults, Account?>> ValidateAccountAsync(string name, string password)
        {
            //return Task.FromResult(new Tuple<AccountLoginResults, Account?>(AccountLoginResults.Success, new Account("1", "1", "admin")));

            return Task.FromResult(new Tuple<AccountLoginResults, Account?>(AccountLoginResults.NotExist, null));
        }

        /// <summary>
        /// 验证刷新Token是否有效
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public Task<Tuple<ValidateRefreshTokenResults, Account?>> ValidateRefreshTokenAsync(string userId, string refreshToken)
        {
            //return Task.FromResult(new Tuple<ValidateRefreshTokenResults, Account?>(ValidateRefreshTokenResults.Success, new Account("1", "1", "admin")));
            return Task.FromResult(new Tuple<ValidateRefreshTokenResults, Account?>(ValidateRefreshTokenResults.Expire, null));
        }

        /// <summary>
        /// 获取账户关联的角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetAccountRoleIdsAsync(string userId)
        {
            IEnumerable<string> result = new List<string>();
            return Task.FromResult(result);
        }
    }
}
