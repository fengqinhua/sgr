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
    }
}
