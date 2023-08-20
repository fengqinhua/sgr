/**************************************************************
 * 
 * 唯一标识：bb6f6ca0-7c1c-452f-9973-64e6fc0e0445
 * 命名空间：Sgr.AuditLogs.ViewModels
 * 创建时间：2023/8/16 17:32:24
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;

namespace Sgr.AuditLogs.ViewModels
{
    public class LogOperateModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string? LoginName { get; set; } = string.Empty;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string? UserName { get; set; } = string.Empty;

        /// <summary>
        /// Ip地址
        /// </summary>
        public string? IpAddress { get; set; }
        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }
        /// <summary>
        /// 客户端浏览器
        /// </summary>
        public string? ClientBrowser { get; set; }
        /// <summary>
        /// 客户端系统
        /// </summary>
        public string? ClientOs { get; set; }
        /// <summary>
        /// 途径（平台、小程序、APP）
        /// </summary>
        public string? OperateWay { get; set; }

        /// <summary>
        /// 请求说明
        /// </summary>
        public string RequestDescription { get; set; } = "";
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; } = "";
        /// <summary>
        /// 请求方法
        /// </summary>
        public string HttpMethod { get; set; } = "";
        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequestParam { get; set; } = "";
        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTimeOffset RequestTime { get; set; }
        /// <summary>
        /// 请求耗时（毫秒）
        /// </summary>
        public long? RequestDuration { get; set; }
        /// <summary>
        /// 请求结果(成功，失败)
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 请求结果描述
        /// </summary>
        public string? Remark { get; set; } = string.Empty;

        public long OrgId { get; set; }
    }
}
