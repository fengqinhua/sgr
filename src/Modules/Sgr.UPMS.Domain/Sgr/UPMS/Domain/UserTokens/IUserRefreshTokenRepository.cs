/**************************************************************
 * 
 * 唯一标识：028b39da-7a3d-4956-8eaa-def3d70ae913
 * 命名空间：Sgr.UPMS.Domain.UserTokens
 * 创建时间：2023/8/28 9:27:25
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Repositories;
using Sgr.UPMS.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Sgr.UPMS.Domain.UserTokens
{
    public interface IUserRefreshTokenRepository : IBaseRepositoryOfTEntityAndTPrimaryKey<UserRefreshToken, long>
    {
        Task<UserRefreshToken?> GetByRefreshTokenAsync(string refreshToken,
            CancellationToken cancellationToken = default);
    }
}
