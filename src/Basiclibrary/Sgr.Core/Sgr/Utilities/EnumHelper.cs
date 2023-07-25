/**************************************************************
 * 
 * 唯一标识：0723f66b-f1e9-461b-b724-ff4f9a25daf4
 * 命名空间：Sgr.Utilities
 * 创建时间：2023/7/24 9:31:24
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Utilities
{
    /// <summary>
    /// 枚举工具类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 字符串转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T StringToEnum<T>(string value, bool ignoreCase = true) where T : struct
        {
            Check.NotNull(value, nameof(value));
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }
    }
}
