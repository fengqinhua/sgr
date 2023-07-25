/**************************************************************
 * 
 * 唯一标识：5beab4e3-817f-43bb-9355-4d419e13d409
 * 命名空间：Sgr
 * 创建时间：2023/7/21 11:58:12
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr
{
    /// <summary>
    /// 检查工具类
    /// </summary>
    public static partial class Check
    {
        /// <summary>
        /// 检查对象是否为NULL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull<T>(T value, string parameterName)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// 检查对象是否为NULL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull<T>(T value, string parameterName, string message)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName, message);
        }

        /// <summary>
        /// 检查字符串是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void StringNotNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"{parameterName} can not be null or empty!", parameterName);
        }

        /// <summary>
        /// 检查字符串是否为空或仅包含空格
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void StringNotNullOrWhiteSpace(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);
        }
    }
}
