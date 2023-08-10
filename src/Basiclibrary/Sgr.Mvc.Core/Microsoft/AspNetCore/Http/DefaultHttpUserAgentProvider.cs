/**************************************************************
 * 
 * 唯一标识：6149122b-8a0b-4043-8296-f020310bd2db
 * 命名空间：Microsoft.AspNetCore.Http
 * 创建时间：2023/8/10 14:51:48
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 缺省的 Http Request User Agent 解析器
    /// <para>如有需要可引入UAParser库以获取更准确的信息！</para>
    /// </summary>
    public class DefaultHttpUserAgentProvider : IHttpUserAgentProvider
    {
        /// <summary>
        /// 分析
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public virtual HttpUserAgentInfo Analysis(HttpContext httpContext)
        {
            HttpUserAgentInfo httpUserAgentInfo = new();

            string userAgent = getUserAgent(httpContext);
            if (!string.IsNullOrEmpty(userAgent))
            {
                httpUserAgentInfo.BrowserInfo = analysisBrowserInfo(userAgent);
                httpUserAgentInfo.Os = analysisOs(userAgent);
                httpUserAgentInfo.OperateWay = getLoginWay(userAgent);
            }

            return httpUserAgentInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected virtual string getUserAgent(HttpContext httpContext)
        {
            var values = httpContext.Request?.Headers?["User-Agent"];
            if (values.HasValue)
                return values!.Value.ToString();
            return string.Empty;
        }

        /// <summary>
        /// 分析浏览器信息
        /// <para>参考;https://www.52dianzi.com/category/article/f4155d2122e122706bc5432e1c5d74ff.html</para>
        /// </summary>
        /// <returns></returns>
        protected virtual string analysisBrowserInfo(string userAgent)
        {
            if (userAgent.Contains("360SE") || userAgent.Contains("360EE"))
                return "360浏览器";

            if (userAgent.Contains("TencentTraveler"))
                return "QQ浏览器";

            if (userAgent.Contains("Chrome"))
                return "Google Chrome";

            if (userAgent.Contains("Firefox"))
                return "Mozilla Firefox";

            if (userAgent.Contains("Safari"))
                return "Apple Safari";

            if (userAgent.Contains("Edge"))
                return "Microsoft Edge";

            if (userAgent.Contains("Trident"))
                return "Internet Explorer";

            if (userAgent.Contains("Opera") || userAgent.Contains("OPR"))
                return "Opera";

            if (userAgent.Contains("Maxthon"))
                return "傲游";

            return "";
        }
        /// <summary>
        /// 分析操作系统
        /// <para>参考;http://www.ejk5.com/news/show-1505.html</para>
        /// </summary>
        /// <returns></returns>
        protected virtual string analysisOs(string userAgent)
        {
            if (userAgent.Contains("NT 10"))
                return "Windows 10 / 11";

            if (userAgent.Contains("NT 6.4"))
                return "Windows 10";

            if (userAgent.Contains("NT 6.2") || userAgent.Contains("NT 6.3"))
                return "Windows 8";

            if (userAgent.Contains("NT 6.1"))
                return "Windows 7 / Server 2008";

            if (userAgent.Contains("Mac"))
                return "Mac";

            if (userAgent.Contains("Unix"))
                return "UNIX";

            if (userAgent.Contains("Linux"))
                return "Linux";

            if (userAgent.Contains("NT 5.1"))
                return "Windows XP";

            if (userAgent.Contains("NT 6.0"))
                return "Windows Vista / Server 2008 R2";

            if (userAgent.Contains("NT 5.2"))
                return "Windows Server 2003";

            if (userAgent.Contains("NT 5.0"))
                return "Windows 2000";

            return "";
        }
        /// <summary>
        /// 分析访问途径
        /// </summary>
        /// <returns></returns>
        protected virtual string getLoginWay(string userAgent)
        {
            if (!string.IsNullOrEmpty(userAgent))
            {
                if (userAgent.Contains("Sgr.DesktopApp"))
                    return "desktop app";

                if (userAgent.Contains("Sgr.Applet"))
                    return "applet app";

                if (userAgent.Contains("Sgr.Mobile"))
                    return "mobile app";
            }

            return "web app";
        }

    }
}
