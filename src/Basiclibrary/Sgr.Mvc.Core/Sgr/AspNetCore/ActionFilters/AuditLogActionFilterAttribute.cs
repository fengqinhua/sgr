/**************************************************************
 * 
 * 唯一标识：9728e461-9515-40c1-bea6-aa4522293fd1
 * 命名空间：Sgr.ActionFilters
 * 创建时间：2023/8/9 7:21:52
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * [ActionName("查询审计日志详情")]
 * [TypeFilter(typeof(AuditLogActionFilterAttribute), Arguments = new object[] { "查询审计日志详情" })]
 * [ServiceFilter(typeof(AuditLogActionFilterAttribute))]
 * [UnAuditLogActionFilter()]
 **************************************************************/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Sgr.Application.Services;
using System;
using System.Threading.Tasks;

namespace Sgr.AspNetCore.ActionFilters
{
    /// <summary>
    /// 审计日志拦截器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AuditLogActionFilterAttribute : Attribute, IAsyncActionFilter
    {
        //[TypeTestFilter(typeof(WeatherForecast), "myparam1", "myparam2", "myparam2", MaxValue = 10, MinValue = 1)]
        //[TypeFilter(typeof(AddHeaderAttribute), Arguments = new object[] { "Author","Ruby" })]

        /// <summary>
        /// 审计日志拦截器
        /// </summary>
        /// <param name="description"></param>
        public AuditLogActionFilterAttribute(string description = "")
        {
            Description = description;
        }

        /// <summary>
        /// 日志描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var auditLogFilterOptions = context.HttpContext.RequestServices.GetRequiredService<IAuditLogFilterOptions>();

            if (!auditLogFilterOptions.IsEnabled || !auditLogFilterOptions.IsNeedAudit(context))
            {
                await next();
                return;
            }

            bool status = true;
            string message = "";
            UserHttpRequestAuditInfo auditInfo = new();
            var auditLogService = context.HttpContext.RequestServices.GetRequiredService<IAuditLogService>();

            try
            {
                await auditLogFilterOptions.Contributor.PreContribute(context.HttpContext, auditInfo, Description);

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
    }
}
