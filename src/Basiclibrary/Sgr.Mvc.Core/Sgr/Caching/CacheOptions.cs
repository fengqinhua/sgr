/**************************************************************
 * 
 * 唯一标识：33e846df-ca3b-4dff-9c62-6fb8a0593b89
 * 命名空间：Sgr.Caching
 * 创建时间：2023/8/23 7:26:44
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
    public class CacheOptions
    {
        /// <summary>
        /// 滑动过期时间（秒数）的缺省值
        /// </summary>
        public int? DefaultSlidingExpirationSecond { get; set; }
        /// <summary>
        /// 绝对过期时间（秒数）的缺省值
        /// </summary>
        public int? DefaultAbsoluteExpirationSecond { get; set; }
    }
}
