/**************************************************************
 * 
 * 唯一标识：52584ce6-6224-481f-acd1-68a683da63a8
 * 命名空间：Sgr.Middlewares
 * 创建时间：2023/8/8 15:21:49
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * [ActionName("查询审计日志详情")]
 * [TypeFilter(typeof(AuditLogActionFilterAttribute), Arguments = new object[] { "查询审计日志详情" })]
 * [ServiceFilter(typeof(AuditLogActionFilterAttribute))]
 * [UnAuditLogActionFilter()]
 **************************************************************/

using Microsoft.AspNetCore.Http;
using Sgr.AuditLogs;
using System;
using System.Threading.Tasks;

namespace Sgr.AspNetCore.Middlewares
{
    /// <summary>
    /// 审计日志中间件
    /// </summary>
    public class AuditLogMiddleware : IMiddleware
    {
        private readonly IAuditLogMiddlewareOptions _auditLogMiddlewareOptions;
        private readonly IAuditLogService _auditLogService;

        /// <summary>
        /// 审计日志中间件
        /// </summary>
        /// <param name="auditLogMiddlewareOptions"></param>
        /// <param name="auditLogService"></param>
        public AuditLogMiddleware(IAuditLogMiddlewareOptions auditLogMiddlewareOptions, IAuditLogService auditLogService)
        {
            _auditLogMiddlewareOptions = auditLogMiddlewareOptions;
            _auditLogService = auditLogService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!_auditLogMiddlewareOptions.IsEnabled || !_auditLogMiddlewareOptions.IsNeedAudit(context))
            {
                await next(context);
                return;
            }

            UserHttpRequestAuditInfo auditInfo = new();
            context.Items[Constant.AUDITLOG_STATU_HTTPCONTEXT_KEY] = auditInfo;

            await _auditLogMiddlewareOptions.Contributor.PreContribute(context, auditInfo);

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                auditInfo.Status = false;
                auditInfo.StatusMessage = ex.Message;
                throw;
            }
            finally
            {
                await _auditLogMiddlewareOptions.Contributor.PostContribute(context, auditInfo);

                if (context.Items.ContainsKey(Constant.AUDITLOG_STATU_HTTPCONTEXT_KEY))
                    await _auditLogService.OperateLogAsync(auditInfo);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientOs"></param>
        /// <returns></returns>
        protected string getLoginWay(string clientOs)
        {
            if (clientOs.Contains("Sgr.DesktopApp"))
                return "desktop app";

            if (clientOs.Contains("Sgr.Applet"))
                return "applet";

            if (clientOs.Contains("Sgr.Mobile"))
                return "mobile app";

            return "web app";
        }
    }
}
