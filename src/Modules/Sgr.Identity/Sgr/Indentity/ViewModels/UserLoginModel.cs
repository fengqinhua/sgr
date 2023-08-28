/**************************************************************
 * 
 * 唯一标识：a3315f55-8de6-4da8-a401-f5f6b585b02e
 * 命名空间：Sgr.Indentity.ViewModels
 * 创建时间：2023/8/21 15:01:49
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.Indentity.ViewModels
{
    public class UserLoginModel
    {
        /// <summary>
        /// 用户名称（可以是登录账号、手机号或邮箱地址）
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; } = string.Empty;
        /// <summary>
        /// 随机数
        /// </summary>
        public string Nonce { get; set; } = string.Empty;
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; } = string.Empty;
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; } = string.Empty;
        /// <summary>
        /// 验证码Hash值
        /// </summary>
        public string VerificationHash { get; set; } = string.Empty;

    }
}
