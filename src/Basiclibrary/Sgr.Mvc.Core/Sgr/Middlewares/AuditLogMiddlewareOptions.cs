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
using Sgr.AuditLog;
using System.Collections.Generic;
using System.Linq;

namespace Sgr.Middlewares
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
            _ignoredUrls = new List<string>();
            Contributor = contributor;

        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 审计信息构建者
        /// </summary>
        public IAuditLogContributor Contributor { get; private set; }

        /// <summary>
        /// 添加可忽略的请求地址
        /// </summary>
        /// <param name="url"></param>
        public void AddIgnoredUrl(string url)
        {
            if (!_ignoredUrls.Contains(url))
                _ignoredUrls.Add(url);
        }
        /// <summary>
        /// 清空可忽略的请求地址
        /// </summary>
        public void ClearIgnoredUrls()
        {
            _ignoredUrls.Clear();
        }

        /// <summary>
        /// 检查是否需要执行审计
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool IsNeedAudit(HttpContext context)
        {
            if (context.Request.Path.Value == null)
                return false;

            var contentTypes = new[] { "application/json", "text/html" };

            if(string.IsNullOrWhiteSpace(context.Response.ContentType) 
                || contentTypes.Any(w => context.Response.ContentType.StartsWith(w)))
            {
                if (!_ignoredUrls.Any(x => context.Request.Path.Value.StartsWith(x)))
                    return true;
            }

            return false;
        }
    }
}
