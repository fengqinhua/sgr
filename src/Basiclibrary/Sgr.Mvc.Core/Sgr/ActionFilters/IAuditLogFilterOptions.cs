/**************************************************************
 * 
 * 唯一标识：d908d7ad-beb0-4515-a64e-2963a256376e
 * 命名空间：Sgr.ActionFilters
 * 创建时间：2023/8/9 7:57:35
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Sgr.AuditLog;

namespace Sgr.ActionFilters
{
    /// <summary>
    /// 审计日志拦截器选项
    /// </summary>
    public interface IAuditLogFilterOptions
    {
        /// <summary>
        /// 审计信息构造
        /// </summary>
        IAuditLogContributor Contributor { get;  }
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
        /// 是否需要执行PageFilter
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        bool IsNeedAudit(PageHandlerExecutingContext context);
        /// <summary>
        /// 是否需要执行ActionFilter
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        bool IsNeedAudit(ActionExecutingContext context);
    }
}
