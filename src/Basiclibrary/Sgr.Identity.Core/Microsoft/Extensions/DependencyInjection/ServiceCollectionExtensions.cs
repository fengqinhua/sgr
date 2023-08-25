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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sgr.Identity;
using Sgr.Identity.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddSgrCookieAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            HttpCookieOptions cookieOpt = configuration.GetSection("Sgr:Identity:Cookie").Get<HttpCookieOptions>() ?? HttpCookieOptions.CreateDefault();
            return AddSgrCookieAuthentication(services, cookieOpt);
        }

        public static AuthenticationBuilder AddSgrCookieAuthentication(this IServiceCollection services,
            HttpCookieOptions cookieOpt)
        {
            services.AddSingleton(cookieOpt);

            return services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
              {
                  //设置存储用户登录信息（用户Token信息）的Cookie名称
                  options.Cookie.Name = cookieOpt.Name;
                  //设置存储用户登录信息（用户Token信息）的Cookie，无法通过客户端浏览器脚本(如JavaScript等)访问到
                  options.Cookie.HttpOnly = cookieOpt.HttpOnly;
                  // 过期时间
                  options.ExpireTimeSpan = TimeSpan.FromSeconds(cookieOpt.ExpireSeconds);
                  //是否在过期时间过半的时候，自动延期
                  options.SlidingExpiration = cookieOpt.SlidingExpiration;
                  //认证失败，会自动跳转到这个地址
                  options.LoginPath = cookieOpt.LoginPath;

                  //options.Events = new CookieAuthenticationEvents
                  //{
                  //    OnRedirectToLogin = context =>
                  //    {
                  //        //IsAjax则返回401
                  //        var xreq = context.Request.Headers.ContainsKey("x-requested-with");
                  //        if (xreq && context.Request.Headers["x-requested-with"] == "XMLHttpRequest")
                  //        {
                  //            var payload = JsonHelper.SerializeObject(new { auth_code = 401, auth_message = "未登录" });
                  //            context.Response.ContentType = "application/json";
                  //            context.Response.StatusCode = 401;
                  //            context.Response.WriteAsync(payload);
                  //        }
                  //        else
                  //        {
                  //            context.Response.Redirect("/OAuth/Login/?ReturnUrl=" + HttpUtility.UrlEncode(context.Request.Path + context.Request.QueryString));
                  //        }

                  //        return Task.CompletedTask;
                  //    }
                  //};
              });
        }


        /// <summary>
        /// 添加JWT认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSgrJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            JwtOptions jwtOpt = configuration.GetSection("Sgr:Identity:JWT").Get<JwtOptions>() ?? JwtOptions.CreateDefault();

            return AddSgrJwtAuthentication(services, jwtOpt);
        }

        /// <summary>
        /// 添加JWT认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="jwtOptions"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSgrJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddSingleton(jwtOptions);
            services.TryAddSingleton<IJwtService, JwtService>();

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

                    x.Events = new JwtBearerEvents
                    {
                        //此处为权限验证失败后触发的事件
                        OnChallenge = context =>
                        {
                            //此处代码为终止.Net Core默认的返回类型和数据结果，这个很重要哦，必须
                            context.HandleResponse();

                            //自定义自己想要返回的数据结果，我这里要返回的是Json对象，通过引用Newtonsoft.Json库进行转换

                            var payload = "{\"serviceerror\": { \"message\": \"无权访问该接口!\" } }";
                            //var payload = JsonHelper.SerializeObject(new { serviceerror  = new { message = "无权访问该接口" } });
                            //自定义返回的数据类型
                            context.Response.ContentType = "application/json";
                            //自定义返回状态码，默认为401 我这里改成 200
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            //输出Json数据结果
                            context.Response.WriteAsync(payload);
                            return Task.FromResult(0);
                        }
                    };
                });
        }

        /// <summary>
        /// 为swagger增加Authentication报文头
        /// </summary>
        /// <param name="option"></param>
        public static void AddSgrAuthenticationHeader(this SwaggerGenOptions option)
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
            ); 

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
