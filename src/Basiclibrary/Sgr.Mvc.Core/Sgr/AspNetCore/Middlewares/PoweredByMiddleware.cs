/**************************************************************
 * 
 * 唯一标识：1364ff3e-4098-4e9f-b4a0-1a0f6ffa265d
 * 命名空间：Sgr.Middlewares
 * 创建时间：2023/8/8 15:16:35
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Sgr.AspNetCore.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class PoweredByMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IPoweredByMiddlewareOptions _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public PoweredByMiddleware(RequestDelegate next, IPoweredByMiddlewareOptions options)
        {
            _next = next;
            _options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task InvokeAsync(HttpContext context)
        {
            if (_options.Enabled)
            {
                context.Response.Headers[_options.HeaderName] = _options.HeaderValue;
            }

            return _next.Invoke(context);
        }
    }




}
