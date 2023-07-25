/**************************************************************
 * 
 * 唯一标识：f39d1d46-1839-4b8e-9d71-adc2a708104e
 * 命名空间：Sgr.Utilities
 * 创建时间：2023/7/24 9:25:57
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 *  参考文章
 *  https://blog.csdn.net/sD7O95O/article/details/86520011
 *  https://github.com/EdiWang/DotNet-Samples/blob/master/AspNet-AES-Non-DPAPI/AspNetAESNonDp/Core/EncryptionService.cs
 **************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sgr.Utilities
{
    /// <summary>
    /// 加解密工具类
    /// </summary>
    public static class EncryptionHelper
    {
        //通过KeyInfo的无参构造函数获得。自己保存下来以后，就可以一直用这一对Key了，保证之后的加解密数据都是一致的
        private static byte[] Key = Convert.FromBase64String("JFBAXvy9WKEQl3opFU/U8wsG8FlESGSGCcHbwFY7w7U=");
        private static byte[] Iv = Convert.FromBase64String("vMu9WHcI+aB78JqskBWjZQ==");

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="outputBase64"></param>
        /// <returns></returns>
        public static string Encrypt(string input, bool outputBase64 = true)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            var enc = EncryptStringToBytes_Aes(input, Key, Iv);

            if (outputBase64)
                return Convert.ToBase64String(enc);
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte b in enc)
                {
                    stringBuilder.AppendFormat("{0:X2}", b);
                }
                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="inputBase64"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, bool inputBase64 = true)
        {
            if (string.IsNullOrEmpty(cipherText))
                return "";


            if (inputBase64)
            {
                var cipherBytes = Convert.FromBase64String(cipherText);
                return DecryptStringFromBytes_Aes(cipherBytes, Key, Iv);
            }
            else
            {
                int num = cipherText.Length / 2;
                byte[] array = new byte[num];
                for (int i = 0; i < num; i++)
                {
                    int num2 = Convert.ToInt32(cipherText.Substring(i * 2, 2), 16);
                    array[i] = (byte)num2;
                }

                return DecryptStringFromBytes_Aes(array, Key, Iv);
            }

        }

        private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));
            byte[] encrypted;

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }
        private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            string plaintext;
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}

