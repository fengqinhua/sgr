/**************************************************************
 * 
 * 唯一标识：c2374085-e9be-48db-b3a6-ae49dbce78ff
 * 命名空间：Sgr.Foundation.API.Application.Queries.AuditLog
 * 创建时间：2023/8/16 17:03:47
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Application.ViewModels;
using System;

namespace Sgr.Foundation.API.Application.Queries.AuditLog
{
    public class InLogOperatePagedRequest: PagedRequest
    {
        /// <summary>
        /// 操作人员
        /// </summary>
        public string? RequestUser { get; set; }
        /// <summary>
        /// 请求说明
        /// </summary>
        public string? RequestDescription { get; set; }
        /// <summary>
        /// 操作时间范围（开始）
        /// </summary>
        public DateTimeOffset? RequestStart { get; set; }
        /// <summary>
        /// 操作时间范围（结束）
        /// </summary>
        public DateTimeOffset? RequestEnd { get; set; }
        /// <summary>
        /// 操作状态
        /// </summary>
        public bool? Status { get; set; }
    }


}
