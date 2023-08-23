/**************************************************************
 * 
 * 唯一标识：ed5c0bbf-8bdd-4a65-808b-f724c76d9e71
 * 命名空间：Sgr.Caching
 * 创建时间：2023/8/23 11:01:18
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Caching
{
    public class CacheEntryOptions
    {
        /// <summary>
        /// 绝对过期时间
        /// </summary>
        public DateTimeOffset? AbsoluteExpiration { get; set; }
        /// <summary>
        /// 绝对过期时间与当前时间的间隔
        /// </summary>
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
        /// <summary>
        /// 滑动过期时间间隔
        /// </summary>
        public TimeSpan? SlidingExpiration { get; set; }

        /// <summary>
        /// 设置滑动过期时间间隔
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public CacheEntryOptions SetSlidingExpirationSecond(int second)
        {
            this.SlidingExpiration = TimeSpan.FromSeconds(second);
            return this;
        }

        /// <summary>
        /// 设置绝对过期时间与当前时间的间隔
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public CacheEntryOptions SetAbsoluteExpirationRelativeToNowSecond(int second)
        {
            this.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(second);
            return this;
        }

        /// <summary>
        /// 设置滑动过期时间间隔
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public CacheEntryOptions SetAbsoluteExpiration(DateTimeOffset dateTime)
        {
            this.AbsoluteExpiration = dateTime;
            return this;
        }


    }
}
