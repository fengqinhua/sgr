/**************************************************************
 * 
 * 唯一标识：b4626b25-e240-4ce1-8ea9-6d5ab683aaef
 * 命名空间：Microsoft.AspNetCore.Http
 * 创建时间：2023/8/10 14:46:53
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Http;

namespace Sgr.AuditLogs
{
    /// <summary>
    /// Http Request User Agent 解析器
    /// </summary>
    public interface IHttpUserAgentProvider
    {
        /// <summary>
        /// 分析
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        HttpUserAgentInfo Analysis(HttpContext httpContext);
    }

    /// <summary>
    /// Http Request 客户端信息
    /// </summary>
    public class HttpUserAgentInfo
    {
        /// <summary>
        /// 客户端浏览器
        /// </summary>
        public string BrowserInfo { get; set; } = string.Empty;
        /// <summary>
        /// 客户端操作系统
        /// </summary>
        public string Os { get; set; } = string.Empty;
        /// <summary>
        /// 访问途径
        /// </summary>
        public string OperateWay { get; set; } = string.Empty;
    }
}
