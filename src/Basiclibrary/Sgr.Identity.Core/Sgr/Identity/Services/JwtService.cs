/**************************************************************
 * 
 * 唯一标识：5ce631b4-6bb2-49c4-80e9-84290665537c
 * 命名空间：Sgr.Identity
 * 创建时间：2023/8/21 11:50:29
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;

namespace Sgr.Identity.Services
{
    public class JwtService : IJwtService
    {

        /// <summary>
        /// 生成刷新Token
        /// </summary>
        /// <returns></returns>
        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// 创建令牌
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string CreateAccessToken(IEnumerable<Claim> claims, JwtOptions options)
        {
            //过期时间
            TimeSpan timeSpan = TimeSpan.FromSeconds(options.ExpireSeconds);
            //加密的token密钥
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
            //签名证书，其值为securityKey和HmacSha256Signature算法
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            //表示jwt token的描述信息，其值包括Issuer签发方，Audience接收方，Claims载荷，过期时间和签名证书
            var tokenDescriptor = new JwtSecurityToken(options.Issuer, 
                options.Audience, 
                claims, 
                expires: DateTime.Now.Add(timeSpan), 
                signingCredentials: credentials);
            //使用该方法转换为字符串形式的jwt token返回
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        
        /// <summary>
        /// 验证令牌
        /// </summary>
        /// <param name="token"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public ClaimsPrincipal? ValidateAccessToken(string token, JwtOptions options)
        {
            if (string.IsNullOrWhiteSpace(token))
                return default;

            token = token.Replace($"{JwtBearerDefaults.AuthenticationScheme} ", "");
            //加密的token密钥
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = options.Issuer,
                ValidAudience = options.Audience,
                IssuerSigningKey = securityKey
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal? claimsPrincipal;

            try
            {
                claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch (SecurityTokenExpiredException)
            {
                //表示过期
                return default;
            }
            catch (SecurityTokenException)
            {
                //表示token错误
                return default;
            }
            catch
            {
                return default;
            }
            return claimsPrincipal;

        }
       
        /// <summary>
        /// 读取令牌信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<Claim>? ReadAccessToken(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    return default;

                token = token.Replace($"{JwtBearerDefaults.AuthenticationScheme} ", "");

                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var readJwtToken = jwtSecurityTokenHandler.ReadJwtToken(token);
                return readJwtToken.Claims;
            }
            catch (Exception)
            {
                return default;
            }
        }

    }
}
