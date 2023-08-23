/**************************************************************
 * 
 * 唯一标识：f07f4c37-e543-4210-bb6b-8c2f0fd9a861
 * 命名空间：Sgr.Application.Services
 * 创建时间：2023/8/21 15:25:54
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Application
{
    public interface ISignatureChecker
    {
        /// <summary>
        /// 验证签名是否正确
        /// </summary>
        /// <remarks>
        ///     签名方法：
        ///     <para>第一步，拼接字符串 "sgr-" + timestamp + "-" + nonce + "-" + customParameters + "-sgr"</para>
        ///     <para>第二步，将字符串做MD5加密</para>   
        ///     <para>第三步，将MD5字符串与 signature 比较看是否一致</para>   
        /// </remarks>
        /// <param name="signature">签名值</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数（一般选择UUID）</param>
        /// <param name="customParameters">自定义参数</param>
        /// <returns></returns>
        bool VerifySignature(string signature,
            string timestamp,
            string nonce,
            string customParameters);
    }
}

