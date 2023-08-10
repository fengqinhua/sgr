/**************************************************************
 * 
 * 唯一标识：d394ab00-a914-4fdf-b1ee-ef4a12a2f2a8
 * 命名空间：Sgr.ActionFilters
 * 创建时间：2023/8/9 10:12:46
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Sgr.AspNetCore.ActionFilters
{
    /// <summary>
    /// 
    /// </summary>
    public class UnAuditLogActionFilterAttribute : Attribute, IAsyncActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Items[Constant.AUDITLOG_STATU_HTTPCONTEXT_KEY] = false;

            await next();
        }
    }
}
