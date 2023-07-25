/**************************************************************
 * 
 * 唯一标识：f4146c3b-c4c0-47ae-9a8d-d8302560d849
 * 命名空间：Sgr.Utilities
 * 创建时间：2023/7/21 18:00:13
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
    /// 字符串工具类
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string? ConvertFromBytesWithoutBom(byte[] bytes, Encoding? encoding = null)
        {
            if (bytes == null)
                return null;

            if (encoding == null)
                encoding = Encoding.UTF8;

            var hasBom = bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;

            if (hasBom)
                return encoding.GetString(bytes, 3, bytes.Length - 3);
            else
                return encoding.GetString(bytes);
        }
    }
}
