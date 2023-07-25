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

    }
}
