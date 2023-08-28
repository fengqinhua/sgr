/**************************************************************
 * 
 * 唯一标识：a2dd5d1d-680c-47e6-9060-d9be14316ccf
 * 命名空间：Sgr.Utilities
 * 创建时间：2023/7/21 18:01:33
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 *    如何确保Random 线程安全？
 *    
 *    方案一
 *    private static int seed = 1024;
 *    private static ThreadLocal<Random> threadLocalRandom 
 *      = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));
 *    
 *    方案二
 *    private static Random _random;
 *    public static Random GetRandom()
 *    {
 *          if (_random == null)
 *          {
 *              byte[] b = new byte[4];
 *              new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
 *              _random = new Random(BitConverter.ToInt32(b, 0));
 *          }
 *    }
 * 
 * 
 * 
 *  最终方案,参考如下文章
 *  
 *  https://devblogs.microsoft.com/pfxteam/getting-random-numbers-in-a-thread-safe-way/
 *  https://andrewlock.net/building-a-thread-safe-random-implementation-for-dotnet-framework/
 *    
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Utilities
{
    /// <summary>
    /// 随机数工具类
    /// </summary>
    public static class RandomHelper
    {
        private static Random _global = new Random();
        [ThreadStatic]
        private static Random? _local;

        private static Random Instance
        {
            get
            {
                if (_local is null)
                {
                    int seed;
                    lock (_global) // 👈 Ensure no concurrent access to Global
                    {
                        seed = _global.Next();
                    }

                    _local = new Random(seed); // 👈 Create [ThreadStatic] instance with specific seed
                }

                return _local;
            }
        }

        public static int GetRandom()
        {
            return Instance.Next();
        }

        public static int GetRandom(int minValue, int maxValue)
        {
            return Instance.Next(minValue, maxValue);
        }

        public static int GetRandom(int maxValue)
        {
            return Instance.Next(maxValue);
        }


        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为不包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public static string GetRandomString(int length = 6,
            bool useNum = false,
            bool useLow = true,
            bool useUpp = true,
            bool useSpe = false,
            string custom = "")
        {

            string s = "", str = custom;
            //if (useNum == true) { str += "0123456789"; }
            //if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            //if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useNum) { str += "123456789"; }
            if (useLow) { str += "abcdefghijklmnpqrstuvwxyz"; }
            if (useUpp) { str += "ABCDEFGHIJKLMNPQRSTUVWXYZ"; }
            if (useSpe) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++)
            {
                s += str.Substring(Instance.Next(0, str.Length - 1), 1);
            }

            return s;
        }
    }
}
