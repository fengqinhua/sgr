/**************************************************************
 * 
 * 唯一标识：40ed2a48-f682-4d71-9d1d-dd269e18cc8a
 * 命名空间：Sgr.ActionFilters
 * 创建时间：2023/8/9 10:21:18
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Sgr.ActionFilters
{
    /// <summary>
    /// 
    /// </summary>
    public class UnAuditLogPageFilterAttribute : Attribute, IAsyncPageFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            context.HttpContext.Items[Constant.AUDITLOG_STATU_HTTPCONTEXT_KEY] = false;

            await next();
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
