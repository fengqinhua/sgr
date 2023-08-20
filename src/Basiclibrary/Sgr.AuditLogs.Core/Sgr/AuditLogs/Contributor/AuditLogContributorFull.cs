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
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Sgr.AuditLogs.Contributor
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditLogContributorFull : AuditLogContributor
    {
        private readonly Stopwatch _stopwatch;
        private readonly IHttpUserAgentProvider _httpUserAgentProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpUserAgentProvider"></param>
        public AuditLogContributorFull(IHttpUserAgentProvider httpUserAgentProvider)
        {
            _stopwatch = new Stopwatch();
            _httpUserAgentProvider = httpUserAgentProvider;
        }

        public override bool IsAuditFull => true;

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

            var httpUserAgent = _httpUserAgentProvider.Analysis(context);
            auditInfo.ClientBrowser = httpUserAgent.BrowserInfo;
            auditInfo.ClientOs = httpUserAgent.Os;
            auditInfo.OperateWay = httpUserAgent.OperateWay;

            auditInfo.Param = await GetHttpBodyAsync(context);
            if (string.IsNullOrEmpty(auditInfo.Param))
                auditInfo.Param = await context.GetHttpFormContentAsync();

            if (string.IsNullOrEmpty(functionDescriptor))
            {
                //var b = context.GetEndpoint()?.Metadata?.GetMetadata<FastEndpoints.EndpointDefinition>();
                //var description = endpoint?.Metadata.GetMetadata<EndpointNameMetadata>()?.EndpointName;

                var endpoint = context.GetEndpoint(); //endpoint.RequestDelegate.Method.ReturnType
                var controllerActionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
                if (controllerActionDescriptor != null)
                    auditInfo.Description = controllerActionDescriptor.ActionName;
                else
                    auditInfo.Description = endpoint?.DisplayName ?? string.Empty;
            }
            else
                auditInfo.Description = functionDescriptor;

            _stopwatch.Restart();
        }


        /// <summary>
        /// 获取请求体内容
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task<string> GetHttpBodyAsync(HttpContext context)
        {
            if (context.Request.ContentType == null || context.Request.ContentType.Contains("multipart/form-data"))
                return "";

            if (context.Request.ContentLength == null || context.Request.ContentLength == 0)
                return "";

            string result = "";
            try
            {
                //context.Request.EnableBuffering();                              //可以实现多次读取Body

                context.Request.Body.Seek(0, SeekOrigin.Begin);

                using var sr = new StreamReader(context.Request.Body);
                result = await sr.ReadToEndAsync();
                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }
            catch { }

            return result;
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
