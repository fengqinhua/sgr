/**************************************************************
 * 
 * 唯一标识：ef509d34-cd62-4145-9830-8f42e49acda9
 * 命名空间：Sgr.AuditLogAggregate
 * 创建时间：2023/8/8 10:26:16
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities.Auditing;
using Sgr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.AuditLogAggregate
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class LogOperate : CreationAuditedEntity<long, long>, IAggregateRoot, IMustHaveOrg<long>
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string LoginName { get; set; } = string.Empty;
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
        public string? LoginWay { get; set; }

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
        public int? RequestDuration { get; set; }
        /// <summary>
        /// 请求结果(成功，失败)
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 请求结果描述
        /// </summary>
        public string? Remark { get; set; } = string.Empty;

        #region IMustHaveOrg  (如果一个实体实现该接口，那么必须存储实体所在组织的ID.)

        /// <summary>
        /// 所属组织
        /// </summary>
        public long OrgId { get; internal protected set; }

        #endregion
    }
}
