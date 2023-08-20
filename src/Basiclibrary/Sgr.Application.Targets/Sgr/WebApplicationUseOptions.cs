/**************************************************************
 * 
 * 唯一标识：6cd06438-47aa-45ef-81c2-85a76ac63a6e
 * 命名空间：Sgr
 * 创建时间：2023/8/20 19:19:55
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.AspNetCore.ActionFilters;
using Sgr.AspNetCore.Middlewares;
using System;

namespace Sgr
{
    public class WebApplicationUseOptions
    {
        public WebApplicationUseOptions()
        {
            EnablePoweredByMiddleware = true;
            PoweredByHeaderValue = Constant.POWEREDBY_HEADERVALUE;
            EnableAuditLogMiddleware = false;
            AuditLogMiddlewareOptionsConfigure = null;
            AuditLogFilterOptionsConfigure = null;

            EnableExceptionHandler = true;
        }
        /// <summary>
        /// 是否启用PoweredBy中间件
        /// </summary>
        public bool EnablePoweredByMiddleware { get; set; }
        /// <summary>
        /// PoweredBy Value
        /// </summary>
        public string PoweredByHeaderValue { get; set; }
        /// <summary>
        /// 是否启用审计日志中间件
        /// </summary>
        public bool EnableAuditLogMiddleware { get; set; }
        /// <summary>
        /// 审计日志中间件参数配置
        /// </summary>
        public Action<IAuditLogMiddlewareOptions>? AuditLogMiddlewareOptionsConfigure { get; set; }
        /// <summary>
        /// 审计日志拦截器参数配置
        /// </summary>
        public Action<IAuditLogFilterOptions>? AuditLogFilterOptionsConfigure { get; set; }
        /// <summary>
        /// 是否启用异常处理中间件
        /// </summary>
        public bool EnableExceptionHandler { get; set; }
        /// <summary>
        /// 异常处理中间件参数配置
        /// </summary>
        public Action<IExceptionHandlingOptions>? ExceptionHandlingOptionsConfigure { get; set; }
    }
}
