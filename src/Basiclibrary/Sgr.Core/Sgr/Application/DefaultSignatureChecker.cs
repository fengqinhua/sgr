/**************************************************************
 * 
 * 唯一标识：6160652f-161c-4d99-92b5-a13b2b2abef1
 * 命名空间：Sgr.Application.Services
 * 创建时间：2023/8/21 15:28:07
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Caching.Services;
using Sgr.Generator;
using Sgr.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Application
{
    public class DefaultSignatureChecker : ISignatureChecker
    {
        private static string key = "Sgr.Application.SignatureChecker.{0}";

        private readonly ICacheManager _cacheManager;

        public DefaultSignatureChecker(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 验证签名是否正确
        /// </summary>
        /// <param name="signature">签名值</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数（一般选择UUID）</param>
        /// <param name="customParameters">自定义参数</param>
        /// <returns></returns>
        public bool VerifySignature(string signature, string timestamp, string nonce, string customParameters)
        {
            //"sgr-" + timestamp + "-" + nonce + "-" + customParameters + "-sgr"
            if (string.IsNullOrEmpty(signature))
                return false;

            //检查随机数nonce是否已验证过
            if (NonceHasBeenVerified(nonce))
                return false;

            //检查时间戳是否有效
            if (!TimestampIsValid(timestamp))
                return false;

            string md5 = HashHelper.CreateMd5($"sgr-{timestamp}-{nonce}-{customParameters}-sgr");

            return signature.Equals(md5, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 随机数（一般选择UUID）是否已验证过
        /// </summary>
        /// <remarks>
        ///  后续可扩展该函数，确保Nonce能且仅能使用一次即失效
        /// </remarks>
        /// <param name="nonce"></param>
        /// <returns></returns>
        protected virtual bool NonceHasBeenVerified(string nonce)
        {
            bool hasKey = true;
            _cacheManager.Get(string.Format(key, nonce), () =>
            {
                hasKey = false;
                return '_';
            },
           _cacheManager.CreateCacheEntryOptions().SetAbsoluteExpirationRelativeToNowSecond(120));

            return hasKey;
        }

        /// <summary>
        /// 时间戳是否有效
        /// </summary>
        /// <remarks>
        ///  后续可扩展该函数，确保时间戳与服务器上的时间保持基本一致
        /// </remarks>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        protected virtual bool TimestampIsValid(string timestamp)
        {
            return true;
        }

    }
}
