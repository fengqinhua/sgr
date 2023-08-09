/**************************************************************
 * 
 * 唯一标识：894157db-cc7e-4270-a6b5-3886b011f1a4
 * 命名空间：Microsoft.AspNetCore.Http
 * 创建时间：2023/8/8 16:49:43
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取UserAgent信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserAgent(this HttpContext context)
        {
            var browser = context.Request?.Headers?["User-Agent"];
            if (browser.HasValue)
                return browser!.Value.ToString();
            else
                return "";
        }

        /// <summary>
        /// 获取Http请求地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRequestUrl(this HttpContext context)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = context.Request.Scheme,
                Host = context.Request.Host.Host,
                Path = context.Request.Path.ToString(),
                Query = context.Request.QueryString.ToString()
            };

            return uriBuilder.Uri.AbsolutePath;
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientIpAddress(this HttpContext context)
        {
            try
            {
                var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (string.IsNullOrEmpty(ip))
                    ip = $"{context?.Connection?.RemoteIpAddress}";
                return ip;
            }
            catch
            {
                return "0.0.0.0";
            }
        }

        /// <summary>
        /// 获取用户申明信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetValueFromClaim(this HttpContext context, string key, string defaultValue)
        {
            var claim = context.User?.FindFirst(key);
            if (claim != null)
                return claim.Value;
            return defaultValue;
        }
    
        /// <summary>
        /// 获取请求体内容
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<string> GetHttpBodyAsync(this HttpContext context)
        {
            if (context.Request.ContentType != null && context.Request.ContentType.Contains("multipart/form-data"))
                return "";


            string result = "";
            try
            {


                context.Request.EnableBuffering();                              //可以实现多次读取Body

                using (var sr = new StreamReader(context.Request.Body))
                {
                    result = await sr.ReadToEndAsync();
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                }
            }
            finally { }

            return result;
        }

        /// <summary>
        /// 获取请求表单信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<string> GetHttpFormContentAsync(this HttpContext context)
        {
            if (context.Request.ContentType != null && context.Request.ContentType.Contains("multipart/form-data"))
                return "";

            string result = "";
            try
            {
                if (context.Request.HasFormContentType)
                {
                    var form = await context.Request.ReadFormAsync();
                    if (form != null)
                    {
                        StringBuilder stringBuilder = new StringBuilder();

                        foreach (var key in form.Keys)
                        {
                            stringBuilder.Append($"key:{key} value:{form[key]}; ");
                        }

                        result = stringBuilder.ToString();
                    }
                }
            }
            finally { }

            return result;
        }



    }
}
