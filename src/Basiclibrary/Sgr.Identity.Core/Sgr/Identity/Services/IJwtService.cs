/**************************************************************
 * 
 * 唯一标识：afd40c24-2fab-4cef-8a9b-47ea759f4611
 * 命名空间：Sgr.Identity
 * 创建时间：2023/8/21 11:50:02
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 *
 *  JWT是由 . 分割的三部分组成：
 *  头部(Header)
 *  载荷(Payload) : 这一部分是JWT主要的信息存储部分，其中包含了许多种的声明（claims）。
 *  签名(Signature)：使用保存在服务端的秘钥对其签名，用来验证发送者的JWT的同时也能确保在期间不被篡改。
 *  
 **************************************************************/

using System.Collections.Generic;
using System.Security.Claims;

namespace Sgr.Identity.Services
{
    public interface IJwtService
    {
        /// <summary>
        /// 创建刷新令牌
        /// </summary>
        /// <returns></returns>
        string CreateRefreshToken();
        /// <summary>
        /// 创建访问令牌
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        string CreateAccessToken(IEnumerable<Claim> claims, JwtOptions options);
        /// <summary>
        /// 验证访问令牌
        /// </summary>
        /// <param name="token"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ClaimsPrincipal? ValidateAccessToken(string token, JwtOptions options);
        /// <summary>
        /// 读取访问令牌信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IEnumerable<Claim>? ReadAccessToken(string token);
    }
}
