/**************************************************************
 * 
 * 唯一标识：a99a76f5-f40b-4edd-8169-7181108691ad
 * 命名空间：Microsoft.Extensions.DependencyInjection
 * 创建时间：2023/8/21 11:39:17
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sgr.Identity;
using Sgr.Identity.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加JWT认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJWTAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            JwtOptions jwtOpt = configuration.GetSection("JWT").Get<JwtOptions>() ??
                new JwtOptions()
                {
                    Audience = "SGR",
                    ExpireSeconds = 60,
                    Issuer = "SGR",
                    Key = Guid.NewGuid().ToString("D").ToUpper()
                };


            return AddJWTAuthentication(services, jwtOpt);
        }
        
        /// <summary>
        /// 添加JWT认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="jwtOptions"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddJWTAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddTransient<IJwtService, JwtService>();
            services.AddSingleton(jwtOptions);

            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,                          //是否验证发行商
                        ValidateAudience = true,                        //是否验证受众者
                        ValidateLifetime = true,                        //是否验证失效时间
                        ValidateIssuerSigningKey = true,                //是否验证签名键
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                    };
                });
        }

        /// <summary>
        /// 为swagger增加Authentication报文头
        /// </summary>
        /// <param name="option"></param>
        public static void AddAuthenticationHeader(this SwaggerGenOptions option)
        {
            option.AddSecurityDefinition("Authorization",
                new OpenApiSecurityScheme
                {
                    Description = "Authorization header. \r\nExample:Bearer 12345ABCDE",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Authorization"
                }
                ); ;

            option.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Authorization"
                        },
                        Scheme="oauth2",
                        Name="Authorization",
                        In=ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        }
    }
}
