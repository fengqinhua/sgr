/**************************************************************
 * 
 * 唯一标识：19881ffc-1e69-4688-8243-d1f327b22389
 * 命名空间：Sgr.Middlewares
 * 创建时间：2023/8/8 15:23:49
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Http;
using Sgr.AuditLogs.Contributor;

namespace Sgr.AspNetCore.Middlewares.AuditLogs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditLogMiddlewareOptions
    {
        /// <summary>
        /// 审计信息构造
        /// </summary>
        IAuditLogContributor Contributor { get; }
        /// <summary>
        /// 是否启用
        /// </summary>
        bool IsEnabled { get; set; }
        /// <summary>
        /// 是否忽略匿名用户的请求
        /// </summary>
        bool IsIgnoreAnonymousUsers { get; set; }
        /// <summary>
        /// 是否忽略HTTP GET请求
        /// </summary>
        bool IsIgnoreGetRequests { get; set; }

        /// <summary>
        /// 清空可忽略的请求地址
        /// </summary>
        void ClearIgnoredUrls();
        /// <summary>
        /// 添加可忽略的请求地址
        /// </summary>
        /// <param name="url"></param>
        void AddIgnoredUrl(string url);

        /// <summary>
        /// 检查是否需要执行审计
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        bool IsNeedAudit(HttpContext context);


    }
}
