/**************************************************************
 * 
 * 唯一标识：fef1d60d-069d-42ac-9de3-8353772e1300
 * 命名空间：Sgr.AspNetCore.Middlewares
 * 创建时间：2023/8/10 18:01:41
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Sgr.AspNetCore.ExceptionHandling;
using Sgr.AuditLogs;
using Sgr.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sgr.AspNetCore.Middlewares
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// 
        /// </summary>
        public static List<Type> ObjectResultTypes { get; }
        static ExceptionHandlingMiddleware()
        {
            ObjectResultTypes = new List<Type>
            {
                typeof(void),
                typeof(JsonResult),
                typeof(ObjectResult),
                typeof(NoContentResult)
            };
        }


        /// <summary>
        /// 异常处理中间件
        /// </summary>
        /// <param name="logger"></param>
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("an exception occurred, but response has already started!" );
                    throw;
                }

                if (ShouldHandleException(context))
                {
                    await HandleAndWrapExceptionAsync(context, ex);
                    return;
                }

                throw ;
            }
        }

        /// <summary>
        /// 是否需要处理当前异常
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual bool ShouldHandleException(HttpContext context)
        {
            var returnType = context.GetEndpoint()?.RequestDelegate?.Method?.ReturnType;
            if (returnType != null)
            {
                returnType = TypeHelper.UnwrapTask(returnType);

                if (!typeof(IActionResult).IsAssignableFrom(returnType))
                {
                    return true;
                }

                return ObjectResultTypes.Any(t => t.IsAssignableFrom(returnType));
            }

            return false;
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected async Task HandleAndWrapExceptionAsync(HttpContext httpContext, Exception exception)
        {
            _logger.LogException(exception);

            //将错误信息记录至审计信息
            if (httpContext.Items[Constant.AUDITLOG_STATU_HTTPCONTEXT_KEY] is UserHttpRequestAuditInfo auditInfo)
            {
                auditInfo.Status = false;
                auditInfo.StatusMessage = exception.Message;
            }

            var converter = httpContext.RequestServices.GetRequiredService<IExceptionToErrorInfo>();

            var errorInfo = converter.Convert(exception);

            httpContext.Response.Clear();
            httpContext.Response.StatusCode = (int)errorInfo.Item1;
            httpContext.Response.OnStarting(ClearCacheHeaders, httpContext.Response);
            httpContext.Response.Headers.Add(Constant.SGR_ERRORHANDLE_HEADERNAME, "true");
            httpContext.Response.Headers.Add("Content-Type", "application/json");

            await httpContext.Response.WriteAsync(
                JsonHelper.SerializeObject(
                    new ServiceErrorResponse(
                        errorInfo.Item2
                    )
                )
            );
        }

        private Task ClearCacheHeaders(object state)
        {
            var response = (HttpResponse)state;

            response.Headers[HeaderNames.CacheControl] = "no-cache";
            response.Headers[HeaderNames.Pragma] = "no-cache";
            response.Headers[HeaderNames.Expires] = "-1";
            response.Headers.Remove(HeaderNames.ETag);

            return Task.CompletedTask;
        }

    }
}
