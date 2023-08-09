/**************************************************************
 * 
 * 唯一标识：ee2006e7-4c93-4145-bc00-dc3be29ad193
 * 命名空间：Sgr.Services
 * 创建时间：2023/8/8 11:16:07
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Services
{
    /// <summary>
    /// 用户请求信息
    /// </summary>
    public class UserHttpRequestAuditInfo
    {

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName{get;set; } = "";
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; } = "";
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTimeOffset OperateTime { get; set; } 
        /// <summary>
        /// 途径
        /// </summary>
        public string OperateWay { get; set; } = "";
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string IpAddress { get; set; } = "";
        /// <summary>
        /// 客户端浏览器
        /// </summary>
        public string ClientBrowser { get; set; } = "";
        /// <summary>
        /// 客户端操作系统
        /// </summary>
        public string ClientOs { get; set; } = "";

        /// <summary>
        /// 请求说明
        /// </summary>
        public string Description { get; set; } = "";
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; } = "";
        /// <summary>
        /// 请求方法
        /// </summary>
        public string HttpMethod { get; set; } = "";
        /// <summary>
        /// 请求参数
        /// </summary>
        public string Param { get; set; } = "";
        /// <summary>
        /// 请求耗时（毫秒）
        /// </summary>
        public long? Duration { get; set; }
    }
}
