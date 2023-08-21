/**************************************************************
 * 
 * 唯一标识：a552cf38-e3a6-4123-9b06-c6a1b1a2157a
 * 命名空间：Sgr.Indentity.ViewModels
 * 创建时间：2023/8/21 15:12:24
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.Indentity.ViewModels
{
    public class TokenModel
    {
        /// <summary>
        /// 令牌类型
        /// </summary>
        public string TokenType { get; set; } = "Bearer";
        /// <summary>
        /// 访问令牌文本内容。注意令牌有过期时间，发现令牌过期后，需要使用RefrashToken重新申请令牌
        /// </summary>
        public string? AccessToken { get; set; }
        /// <summary>
        /// 刷新令牌文本内容我
        /// </summary>
        public string? RefrashToken { get; set; }
    }
}
