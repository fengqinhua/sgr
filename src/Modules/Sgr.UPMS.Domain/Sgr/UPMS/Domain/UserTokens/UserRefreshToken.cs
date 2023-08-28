/**************************************************************
 * 
 * 唯一标识：1f857775-884f-4525-a1ef-9d1332b62ad9
 * 命名空间：Sgr.UPMS.Domain.UserTokens
 * 创建时间：2023/8/28 9:22:43
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;
using Sgr.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.UPMS.Domain.UserTokens
{
    public class UserRefreshToken : Entity<long>, IAggregateRoot
    {
        public UserRefreshToken() { }

        public UserRefreshToken(string token, DateTimeOffset expires, long userId)
            : this()
        {
            Token = token;
            Expires = expires;
            UserId = userId;
        }

        /// <summary>
        /// 令牌内容
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTimeOffset Expires { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 是否已过期
        /// </summary>
        /// <returns></returns>
        public bool IsExpire()
        {
            return DateTimeOffset.UtcNow <= Expires;
        } 
    }
}
