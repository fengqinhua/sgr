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

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class HttpContextExtensions
    {
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
        /// 获取请求表单信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<string> GetHttpFormContentAsync(this HttpContext context)
        {
            if (context.Request.ContentType == null || context.Request.ContentType.Contains("multipart/form-data"))
                return "";

            string result = "";
            try
            {
                if (context.Request.HasFormContentType)
                {
                    var form = await context.Request.ReadFormAsync();
                    if (form != null)
                    {
                        StringBuilder stringBuilder = new ();
                        
                        foreach (var key in form)
                        {
                            stringBuilder.Append($"key:{key.Key} value:{key.Value}; ");
                        }

                        result = stringBuilder.ToString();
                    }
                }
            }
            catch { }

            return result;
        }



    }
}
