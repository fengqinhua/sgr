/**************************************************************
 * 
 * 唯一标识：b9cdff8a-3a3c-44dd-8f86-a1fb0b7be32b
 * 命名空间：Sgr.Middlewares
 * 创建时间：2023/8/8 17:45:53
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Http;
using Sgr.Services;
using System;
using System.Threading.Tasks;

namespace Sgr.AuditLog
{
    /// <summary>
    /// 审计信息构建者(基础版)
    /// </summary>
    public class AuditLogContributor : IAuditLogContributor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="auditInfo"></param>
        /// <param name="functionDescriptor"></param>
        /// <returns></returns>
        public virtual Task PreContribute(HttpContext context, UserHttpRequestAuditInfo auditInfo, string functionDescriptor = "")
        {
            auditInfo.LoginName = context.GetValueFromClaim(Constant.CLAIM_LOGIN_NAME, "");
            auditInfo.UserName = context.GetValueFromClaim(Constant.CLAIM_USER_NAME, "");
            auditInfo.IpAddress = context.GetClientIpAddress();

            auditInfo.HttpMethod = context.Request.Method;
            auditInfo.Url = context.GetRequestUrl();
            auditInfo.OperateTime = DateTimeOffset.Now;

            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="auditInfo"></param>
        public virtual Task PostContribute(HttpContext context, UserHttpRequestAuditInfo auditInfo)
        {
            return Task.CompletedTask;
        }


    }
}
