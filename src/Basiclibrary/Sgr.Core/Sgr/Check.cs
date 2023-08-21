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
        /// <param name="message"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull<T>(T value, string parameterName, string? message = null)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName, message ?? $"required input {parameterName} was null.");
        }

        #region string

        /// <summary>
        /// 检查字符串是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void StringNotNullOrEmpty(string? value, string? parameterName, string? message = null)
        {
            if (value == null || value.Length == 0)
                throw new ArgumentException(message ?? $"{parameterName} can not be null or empty!", parameterName);
        }

        /// <summary>
        /// 检查字符串是否为空或仅包含空格
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void StringNotNullOrWhiteSpace(string? value, string? parameterName, string? message = null)
        {
            if (value == null || string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(message ?? $"{parameterName} can not be null, empty or white space!", parameterName);
        }
        
        /// <summary>
        /// 检查字符串是否超过指定长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void StringNotOverLength(string value,  int maxLength, string parameterName, string? message = null)
        {
            if ( value.Length > maxLength)
                throw new ArgumentException(message ?? $"{parameterName} length should be  less than {maxLength}!", parameterName);
        }

        /// <summary>
        /// 检查字符串是否不在指定范围
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void StringNotOverLength(string value, int minLength, int maxLength, string parameterName, string? message = null)
        {
            if(value.Length < minLength || value.Length > maxLength)
                throw new ArgumentException(message ?? $"{parameterName} length should be between {minLength} to {maxLength}!", parameterName);
        }


        #endregion


        #region Number

        /// <summary>
        /// 输入参数不为零
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero(int input,string parameterName,string? message = null)
        {
            if(input == 0)
                throw new ArgumentException(message ?? $"required input {parameterName} cannot be zero!", parameterName);
        }

        /// <summary>
        /// 输入参数不为零
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero(long input, string parameterName, string? message = null)
        {
            if (input == 0)
                throw new ArgumentException(message ?? $"required input {parameterName} cannot be zero!", parameterName);
        }

        /// <summary>
        /// 输入参数不为零
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero(decimal input, string parameterName, string? message = null)
        {
            if (input == 0)
                throw new ArgumentException(message ?? $"required input {parameterName} cannot be zero!", parameterName);
        }

        /// <summary>
        /// 输入参数不为零
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero(float input, string parameterName, string? message = null)
        {
            if (input == 0)
                throw new ArgumentException(message ?? $"required input {parameterName} cannot be zero!", parameterName);
        }

        /// <summary>
        /// 输入参数不为零
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void Zero(double input, string parameterName, string? message = null)
        {
            if (input == 0)
                throw new ArgumentException(message ?? $"required input {parameterName} cannot be zero!", parameterName);
        }


        #endregion
    }
}
