/**************************************************************
 * 
 * 唯一标识：6ec5bb84-c55d-4e4c-b268-93d070967da0
 * 命名空间：Sgr.ActionFilters
 * 创建时间：2023/8/9 7:50:59
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Sgr.AspNetCore.AuditLog;
using System;
using System.Collections.Generic;

namespace Sgr.AspNetCore.ActionFilters
{
    /// <summary>
    /// 日志审计中间件选项
    /// </summary>
    public class AuditLogFilterOptions : IAuditLogFilterOptions
    {
        /// <summary>
        /// 日志审计中间件选项
        /// </summary>
        /// <param name="contributor"></param>
        public AuditLogFilterOptions(IAuditLogContributor contributor)
        {
            Contributor = contributor;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsEnabled { get; set; } = true;
        /// <summary>
        /// 是否忽略匿名用户的请求
        /// </summary>
        public virtual bool IsIgnoreAnonymousUsers { get; set; } = true;
        /// <summary>
        /// 是否忽略HTTP GET请求
        /// </summary>
        public virtual bool IsIgnoreGetRequests { get; set; } = true;

        /// <summary>
        /// 审计信息构建者
        /// </summary>
        public virtual IAuditLogContributor Contributor { get; private set; }

        /// <summary>
        /// 是否需要执行PageFilter
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool IsNeedAudit(PageHandlerExecutingContext context)
        {
            if (context.HttpContext.Items[Constant.AUDITLOG_STATU_HTTPCONTEXT_KEY] is bool hasKey && hasKey)
                return false;

            if (!context.ActionDescriptor.IsPageAction())
                return false;

            //检查是否需要忽略GET请求
            if (IsIgnoreGetRequests
                && string.Equals(context.HttpContext.Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
                return false;

            //检查是否需要忽略匿名的用户请求
            if (IsIgnoreAnonymousUsers
                && context.HttpContext.GetValueFromClaim(Constant.CLAIM_USER_ID, "").Length == 0)
                return false;


            return false;
        }

        /// <summary>
        /// 是否需要执行ActionFilter
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool IsNeedAudit(ActionExecutingContext context)
        {
            if (context.HttpContext.Items[Constant.AUDITLOG_STATU_HTTPCONTEXT_KEY] is bool hasKey && hasKey)
                return false;

            if (!context.ActionDescriptor.IsControllerAction())
                return false;


            //检查是否需要忽略GET请求
            if (IsIgnoreGetRequests
                && string.Equals(context.HttpContext.Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
                return false;

            //检查是否需要忽略匿名的用户请求
            if (IsIgnoreAnonymousUsers
                && context.HttpContext.GetValueFromClaim(Constant.CLAIM_USER_ID, "").Length == 0)
                return false;

            return true;
        }
    }
}
