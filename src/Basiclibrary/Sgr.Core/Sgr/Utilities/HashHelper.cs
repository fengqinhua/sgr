/**************************************************************
 * 
 * 唯一标识：a1d622f0-5b9f-45b2-bcb5-eb0e4c2f151d
 * 命名空间：Sgr.Utilities
 * 创建时间：2023/7/24 9:50:54
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace Sgr.Utilities
{
    /// <summary>
    /// Hash工具类
    /// </summary>
    public static class HashHelper
    {
        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string CreateMd5(string inputValue)
        {
            return CreateMd5(Encoding.UTF8.GetBytes(inputValue));
        }

        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string CreateMd5(object inputValue)
        {
            if (inputValue == null)
                return "";

            byte[] buff;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                iFormatter.Serialize(ms, inputValue);
                buff = ms.GetBuffer();
            }

            if (buff == null || buff.Length == 0)
                return "";

            return CreateMd5(buff);
        }

        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        public static string CreateMd5(byte[] buff)
        {
            Check.NotNull(buff, nameof(buff));

            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(buff);

                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");

                //var sb = new StringBuilder();
                //foreach (var hashByte in result)
                //{
                //    sb.Append(hashByte.ToString("X2"));
                //}
            }
        }

        ///// <summary>
        ///// 创建数据哈希
        ///// </summary>
        ///// <param name="data">用于计算哈希的数据</param>
        ///// <param name="hashAlgorithm">哈希算法</param>
        ///// <param name="trimByteCount">哈希算法中将使用的字节数</param>
        ///// <returns>Data hash</returns>
        //public static string CreateHash(byte[] data, string hashAlgorithm, int trimByteCount = 0)
        //{
        //    if (string.IsNullOrEmpty(hashAlgorithm))
        //        throw new ArgumentNullException(nameof(hashAlgorithm));

        //    var algorithm = (HashAlgorithm)CryptoConfig.CreateFromName(hashAlgorithm);
        //    if (algorithm == null)
        //        throw new ArgumentException("Unrecognized hash name");

        //    if (trimByteCount > 0 && data.Length > trimByteCount)
        //    {
        //        var newData = new byte[trimByteCount];
        //        Array.Copy(data, newData, trimByteCount);

        //        return BitConverter.ToString(algorithm.ComputeHash(newData)).Replace("-", string.Empty);
        //    }

        //    return BitConverter.ToString(algorithm.ComputeHash(data)).Replace("-", string.Empty);
        //}
    }
}
