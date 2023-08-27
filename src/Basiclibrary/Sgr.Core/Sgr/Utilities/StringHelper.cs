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
using System.Text.RegularExpressions;

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

            encoding ??= Encoding.UTF8;

            var hasBom = bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF;

            if (hasBom)
                return encoding.GetString(bytes, 3, bytes.Length - 3);
            else
                return encoding.GetString(bytes);
        }

        public static string SubStringMaxLength(string str, int maxLength)
        {
            maxLength -= 6;
            if (maxLength < 0)
                maxLength = 1;

            if (string.IsNullOrEmpty(str) || str.Length < maxLength)
                return str;

            return str.Substring(0, maxLength) + "...";
        }

        /// <summary>
        /// 密码长度为8-14个字符且数字、字母以及标点符号需至少包含两种，不允许有空格、中文
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool SgrPasswordComplexityIsCompliant(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (password.Length < 8)
                return false; //"密码长度至少需要8位";

            if (password.Length > 14)
                return false; //"密码长度不可超过14位";

            if (password.IndexOf(" ") >= 0)
                return false; //"密码中不允许有空格";

            if (Regex.Matches(password, "[\u4e00-\u9fa5]").Count > 0)
                return false; //"密码中不允许有中文";

            int hasDigit = 0;             // Regex.Matches(pwd, "[0-9]").Count > 0 ? 1 : 0;                  
            int hasLetter = 0;            // Regex.Matches(pwd, "[a-zA-Z]").Count > 0 ? 1 : 0;               
            int hasPunctuation = 0;       //包含标点符号 
            foreach (var item in password)
            {
                //包含数字
                if (char.IsDigit(item))
                    hasDigit = 1;

                //包含字母
                if (char.IsLetter(item))
                    hasLetter = 1;

                //包含标点
                if (char.IsPunctuation(item))
                    hasPunctuation = 1;
            }

            if ((hasDigit + hasLetter + hasPunctuation) < 2)
                return false; //"密码中数字、字母以及标点符号需至少包含两种";

            return true;
        }


        /// <summary>
        /// 密码复杂度是否合规
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="hasLetter">是否必须包含字母</param>
        /// <param name="hasSpecialCharacters">是否必须包含特殊字符</param>
        /// <returns></returns>
        public static bool PasswordComplexityIsCompliant(string password,
            bool hasLetter = true,
            bool hasSpecialCharacters = true)
        {
            string strCompliant = "(?=.*[0-9])";

            if (hasLetter)
                strCompliant += "(?=.*[a-zA-Z])";

            if (hasSpecialCharacters)
                strCompliant += "(?=([\x21-\x7e]+)[^a-zA-Z0-9])";

            var regex = new Regex(strCompliant, RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            return regex.IsMatch(password);

            //        //密码复杂度正则表达式
            //        var regex = new Regex(@"
            //(?=.*[0-9])                     #必须包含数字
            //(?=.*[a-zA-Z])                  #必须包含小写或大写字母
            //(?=([\x21-\x7e]+)[^a-zA-Z0-9])  #必须包含特殊符号
            //.{8,30}                         #至少8个字符，最多30个字符
            //", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            //        //校验密码是否符合
            //        bool pwdIsMatch = regex.IsMatch(user.USER_PASS)
        }
    }
}
