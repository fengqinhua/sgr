/**************************************************************
 * 
 * 唯一标识：751784e9-0d20-465c-898b-38e93741a5d7
 * 命名空间：Sgr.Services
 * 创建时间：2023/8/8 11:09:36
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Services
{
    /// <summary>
    /// 审计日志服务
    /// </summary>
    public interface IAuditLogService
    {
        /// <summary>
        /// 记录用户操作审计日志
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        Task OperateLogAsync(UserHttpRequestAuditInfo requestInfo, bool status, string? message);
    }


}
