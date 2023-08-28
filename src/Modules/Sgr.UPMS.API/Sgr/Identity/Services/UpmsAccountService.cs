/**************************************************************
 * 
 * 唯一标识：772e1481-accf-462e-abce-7dd64b1955c4
 * 命名空间：Sgr.Identity.Services
 * 创建时间：2023/8/28 9:17:42
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Exceptions;
using Sgr.UPMS;
using Sgr.UPMS.Domain.LogLogins;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Domain.UserTokens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sgr.Identity.Services
{
    public class UpmsAccountService : IAccountService
    {
        private readonly ILogLoginRepository _logLoginRepository;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly UpmsOptions _upmsOptions;

        public UpmsAccountService(IUserRepository userRepository, ILogLoginRepository logLoginRepository, IUserRefreshTokenRepository userRefreshTokenRepository,  UpmsOptions upmsOptions)
        {
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _userRepository = userRepository;
            _upmsOptions = upmsOptions;
            _logLoginRepository = logLoginRepository;
        }

        public async Task CreateRefreshTokenAsync(string userId, string refreshToken, int minutes = 720)
        {
            if (!long.TryParse(userId, out long id))
                throw new BusinessException($"userId {userId} 格式错误");

            UserRefreshToken userRefreshToken = new UserRefreshToken(refreshToken, DateTimeOffset.UtcNow.AddMinutes(720), id);
            await _userRefreshTokenRepository.InsertAsync(userRefreshToken);
            await _userRefreshTokenRepository.UnitOfWork.SaveEntitiesAsync();
        }

        public async Task<Tuple<AccountLoginResults, Account?>> ValidateAccountAsync(string loginName, string password)
        {
            User? user = await _userRepository.GetByLoginNameAsync(loginName);

            if (user == null)
                return new Tuple<AccountLoginResults, Account?>(AccountLoginResults.NotExist, null);

            if(user.IsDeleted)
                return new Tuple<AccountLoginResults, Account?>(AccountLoginResults.IsDelete, null);

            if (user.State != Domain.Entities.EntityStates.Normal)
                return new Tuple<AccountLoginResults, Account?>(AccountLoginResults.IsDeactivate, null);

            if(user.CannotLoginUntilUtc.HasValue && user.CannotLoginUntilUtc.Value >DateTimeOffset.UtcNow)
                return new Tuple<AccountLoginResults, Account?>(AccountLoginResults.IsLock, null);

            if (user.CheckPassWord(password))
            {
                //登录成功
                user.LoginSuccess();

                await _userRepository.UpdateAsync(user);
                await _userRepository.UnitOfWork.SaveEntitiesAsync();

                return new Tuple<AccountLoginResults, Account?>(AccountLoginResults.Success,
                    new Account($"{user.Id}", $"{user.OrgId}", user.LoginName));
            }
            else
            {

                //登录失败
                user.LoginFail();
                //连续登录失败次数超过上限则锁定账号
                if (_upmsOptions.FailedPasswordAllowedAttempts > 0
                    && user.FailedLoginAttempts > _upmsOptions.FailedPasswordAllowedAttempts)
                {
                    user.LoginLock(DateTimeOffset.UtcNow.AddMinutes(_upmsOptions.FailedPasswordLockoutMinutes));
                }

                await _userRepository.UpdateAsync(user);
                await _userRepository.UnitOfWork.SaveEntitiesAsync();
                return new Tuple<AccountLoginResults, Account?>(AccountLoginResults.WrongPassword, null);
            }
        }

        public async Task<Tuple<ValidateRefreshTokenResults, Account?>> ValidateRefreshTokenAsync(string userId, string refreshToken)
        {
            if(string.IsNullOrEmpty(refreshToken))
                return new Tuple<ValidateRefreshTokenResults, Account?>(ValidateRefreshTokenResults.NotExist, null);

            UserRefreshToken? userRefreshToken = await _userRefreshTokenRepository.GetByRefreshTokenAsync(refreshToken);

            if (userRefreshToken == null)
                return new Tuple<ValidateRefreshTokenResults, Account?>(ValidateRefreshTokenResults.NotExist, null);

            if (userRefreshToken.IsExpire())
                return new Tuple<ValidateRefreshTokenResults, Account?>(ValidateRefreshTokenResults.Expire, null);

            if (!long.TryParse(userId, out long id))
                throw new BusinessException($"userId {userId} 格式错误");

            if ( userRefreshToken.UserId != id)
                return new Tuple<ValidateRefreshTokenResults, Account?>(ValidateRefreshTokenResults.NotExist, null);

            User? user = await _userRepository.GetAsync(id);

            if (user == null
                || user.IsDeleted 
                || user.State != Domain.Entities.EntityStates.Normal 
                || (user.CannotLoginUntilUtc.HasValue && user.CannotLoginUntilUtc.Value > DateTimeOffset.UtcNow))
                return new Tuple<ValidateRefreshTokenResults, Account?>(ValidateRefreshTokenResults.AccountAbnormal, null);

            return new Tuple<ValidateRefreshTokenResults, Account?>(ValidateRefreshTokenResults.Success,
                 new Account($"{user.Id}", $"{user.OrgId}", user.LoginName));
        }

        public async Task CreateLoginLog(string loginName, string ipAddress, string loginWay, string clientBrowser, string clientOs, bool status, string remark, string orgId)
        {
            if (!long.TryParse(orgId, out long oid))
                oid = 0;

            LogLogin logLogin = new LogLogin(loginName, oid)
            {
                ClientOs = clientOs,
                IpAddress = ipAddress,
                LoginWay = loginWay,
                LoginTime = DateTimeOffset.UtcNow,
                UserName = loginName,
                Status = status,
                Remark = remark,
                ClientBrowser = clientBrowser
            };
            await _logLoginRepository.InsertAsync(logLogin);
            await _logLoginRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
