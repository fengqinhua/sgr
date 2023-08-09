/**************************************************************
 * 
 * 唯一标识：8d39f8ed-91c9-4fda-939b-bed6125a7aeb
 * 命名空间：Sgr.Middlewares
 * 创建时间：2023/8/8 20:05:17
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Http;
using Sgr.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sgr.AuditLog
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditLogContributorAll : AuditLogContributor
    {
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// 
        /// </summary>
        public AuditLogContributorAll()
        {
            _stopwatch = new Stopwatch();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="auditInfo"></param>
        /// <param name="functionDescriptor"></param>
        /// <returns></returns>
        public override async Task PreContribute(HttpContext context, UserHttpRequestAuditInfo auditInfo, string functionDescriptor = "")
        {
            await base.PreContribute(context, auditInfo);
            
            string ua = context.GetUserAgent();
            auditInfo.ClientBrowser = ua;
            //auditInfo.ClientOs= context.get();
            //auditInfo.OperateWay = getLoginWay(auditInfo.ClientBrowser);

            auditInfo.Param = await context.GetHttpBodyAsync();
            if (string.IsNullOrEmpty(auditInfo.Param))
                auditInfo.Param = await context.GetHttpFormContentAsync();

            if (string.IsNullOrEmpty(functionDescriptor))
            {
                var controllerDescriptorAttribute = context.GetEndpoint()?.Metadata?.GetMetadata<FunctionDescriptorAttribute>();
                auditInfo.Description = controllerDescriptorAttribute?.FunctionName ?? string.Empty;
            }
            else
                auditInfo.Description = functionDescriptor;

            _stopwatch.Restart();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="auditInfo"></param>
        public override async Task PostContribute(HttpContext context, UserHttpRequestAuditInfo auditInfo)
        {
            _stopwatch.Stop();
            auditInfo.Duration = _stopwatch.ElapsedMilliseconds;

            await base.PostContribute(context, auditInfo);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="clientOs"></param>
        ///// <returns></returns>
        //protected string getLoginWay(string clientOs)
        //{
        //    if (clientOs.Contains("Sgr.DesktopApp"))
        //        return "desktop app";

        //    if (clientOs.Contains("Sgr.Applet"))
        //        return "applet";

        //    if (clientOs.Contains("Sgr.Mobile"))
        //        return "mobile app";

        //    return "web app";
        //}
    }
}
