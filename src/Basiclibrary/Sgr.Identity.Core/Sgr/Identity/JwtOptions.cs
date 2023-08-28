/**************************************************************
 * 
 * 唯一标识：8564b939-8576-4f30-bb59-6a72accfaf46
 * 命名空间：Sgr.Identity
 * 创建时间：2023/8/21 11:40:40
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;

namespace Sgr.Identity
{
    /// <summary>
    /// JWT配置参数
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// 签发者
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// 过期时间
        /// </summary>
        public int ExpireSeconds { get; set; }

        /// <summary>
        /// 是否启用签名验证
        /// </summary>
        public bool UseSignature { get; set; }
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool UseCaptcha { get; set; }
        /// <summary>
        /// 验证码是否为算术公式
        /// </summary>
        public bool CaptchaIsArithmetic { get; set; }

        /// <summary>
        /// 创建缺省配置项
        /// </summary>
        /// <returns></returns>
        public static JwtOptions CreateDefault()
        {
            return new JwtOptions()
            {
                Audience = "SGR",
                ExpireSeconds = 60,
                Issuer = "SGR",
                Key = Guid.NewGuid().ToString("D").ToUpper(),
                UseSignature = false,
                UseCaptcha = false,
                CaptchaIsArithmetic = true
            };
        }
    }
}
