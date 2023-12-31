﻿/**************************************************************
 * 
 * 唯一标识：7b135190-3b06-458a-b38d-f716e4b65322
 * 命名空间：Sgr.Services
 * 创建时间：2023/8/8 11:18:13
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.AuditLogs.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultAuditLogService : IAuditLogService
    {
        private readonly ILogger<DefaultAuditLogService> logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public DefaultAuditLogService(ILogger<DefaultAuditLogService> logger) { this.logger = logger; }


        /// <summary>
        /// 记录用户操作审计日志
        /// </summary>
        /// <param name="requestInfo"></param>
        public Task OperateLogAsync(UserHttpRequestAuditInfo requestInfo)
        {
            logger.LogInformation($"{requestInfo.OperateTime} {requestInfo.LoginName} 通过 {requestInfo.OperateWay ?? "平台"} 执行请求 {(requestInfo.Status ? "成功" : "失败，原因：" + requestInfo.StatusMessage ?? "未知" + "）")}");
            logger.LogInformation($"----> 用户姓名：{requestInfo?.UserName} ； 客户端IP：{requestInfo?.IpAddress} ； 客户端浏览器：{requestInfo?.ClientBrowser} ； 客户端操作系统：{requestInfo?.ClientOs}");
            logger.LogInformation($"----> 请求描述：{requestInfo?.Description}");
            logger.LogInformation($"----> 请求方式：{requestInfo?.HttpMethod}");
            logger.LogInformation($"----> 请求地址：{requestInfo?.Url}");
            logger.LogInformation($"----> 请求参数：{requestInfo?.Param}");
            logger.LogInformation($"----> 请求耗时：{requestInfo?.Duration}毫秒");

            return Task.CompletedTask;
        }
    }
}
