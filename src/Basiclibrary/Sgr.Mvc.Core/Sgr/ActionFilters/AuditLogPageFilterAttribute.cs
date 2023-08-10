﻿/**************************************************************
 * 
 * 唯一标识：6c07c3e7-c736-4070-b269-c58d9a9a8a15
 * 命名空间：Sgr.ActionFilters
 * 创建时间：2023/8/9 10:05:57
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Sgr.Services;
using System;
using System.Threading.Tasks;

namespace Sgr.ActionFilters
{
    /// <summary>
    /// 审计日志拦截器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AuditLogPageFilterAttribute : Attribute, IAsyncPageFilter
    {
        /// <summary>
        /// 审计日志拦截器
        /// </summary>
        /// <param name="description"></param>
        public AuditLogPageFilterAttribute(string description = "")
        {
            Description = description;
        }

        /// <summary>
        /// 日志描述
        /// </summary>
        public string Description { get;private set; }  

        /// <summary>
        /// 在调用处理程序方法之前，在模型绑定完成后异步调用。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var auditLogFilterOptions = context.HttpContext.RequestServices.GetRequiredService<IAuditLogFilterOptions>();

            if (!auditLogFilterOptions.IsEnabled || !auditLogFilterOptions.IsNeedAudit(context))
            {
                await next();
                return;
            }

            bool status = true;
            string message = "";
            UserHttpRequestAuditInfo auditInfo = new UserHttpRequestAuditInfo();

            var auditLogService = context.HttpContext.RequestServices.GetRequiredService<IAuditLogService>();

            try
            {
                await auditLogFilterOptions.Contributor.PreContribute(context.HttpContext, auditInfo, this.Description);

                await next();

                await auditLogFilterOptions.Contributor.PostContribute(context.HttpContext, auditInfo);
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
                throw;
            }
            finally
            {
                await auditLogService.OperateLogAsync(auditInfo, status, message);
            }
        }

        /// <summary>
        /// 在选择处理程序方法之后，但在模型绑定发生之前异步调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }
    }
}