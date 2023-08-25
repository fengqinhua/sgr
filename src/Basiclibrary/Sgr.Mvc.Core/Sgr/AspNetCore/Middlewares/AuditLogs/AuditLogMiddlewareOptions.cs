/**************************************************************
 * 
 * 唯一标识：6b9d6744-b2f2-4150-aff7-d313acaadbb9
 * 命名空间：Sgr.Middlewares
 * 创建时间：2023/8/8 16:32:00
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Http;
using Sgr.AuditLogs.Contributor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sgr.AspNetCore.Middlewares.AuditLogs
{
    /// <summary>
    /// 日志审计中间件选项
    /// </summary>
    public class AuditLogMiddlewareOptions : IAuditLogMiddlewareOptions
    {
        private readonly IList<string> _ignoredUrls;


        /// <summary>
        /// 日志审计中间件选项
        /// </summary>
        /// <param name="contributor"></param>
        public AuditLogMiddlewareOptions(IAuditLogContributor contributor)
        {
            _ignoredUrls = new List<string>() { };
            initDefaultIgnoredUrls();

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
        /// 添加可忽略的请求地址
        /// </summary>
        /// <param name="url"></param>
        public virtual void AddIgnoredUrl(string url)
        {
            if (!_ignoredUrls.Contains(url))
                _ignoredUrls.Add(url);
        }
        /// <summary>
        /// 清空可忽略的请求地址
        /// </summary>
        public virtual void ClearIgnoredUrls()
        {
            _ignoredUrls.Clear();
            initDefaultIgnoredUrls();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void initDefaultIgnoredUrls()
        {
            _ignoredUrls.Add("/favicon.ico");

        }

        /// <summary>
        /// 检查是否需要执行审计
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool IsNeedAudit(HttpContext context)
        {
            if (context.Request.Path.Value == null || context.Request.Path.Value == "/")
                return false;

            //检查是否需要忽略GET请求
            if (IsIgnoreGetRequests
                && string.Equals(context.Request.Method, "GET", StringComparison.OrdinalIgnoreCase))
                return false;

            //检查是否需要忽略匿名的用户请求
            if (IsIgnoreAnonymousUsers
                && context.GetValueFromClaim(Constant.CLAIM_USER_ID, "").Length == 0)
                return false;

            //检查是否需要忽略指定的ContentType
            //var contentTypes = new[] { "application/json", "text/html" };
            //if (string.IsNullOrWhiteSpace(context.Response.ContentType)
            //    || contentTypes.Any(w => context.Response.ContentType.StartsWith(w)))
            //{

            //}

            //检查是否满足忽略的地址清单
            if (_ignoredUrls.Any(x => context.Request.Path.Value.StartsWith(x)))
                return false;

            return true;
        }
    }
}
