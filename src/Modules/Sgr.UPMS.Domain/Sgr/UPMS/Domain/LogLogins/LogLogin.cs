/**************************************************************
 * 
 * 唯一标识：042db49f-21d0-4f79-98b9-a5c3130e2a17
 * 命名空间：Sgr.AuditLogAggregate
 * 创建时间：2023/8/8 10:26:05
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;
using Sgr.Domain.Entities.Auditing;
using System;

namespace Sgr.UPMS.Domain.LogLogins
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LogLogin : CreationAuditedEntity<long, long>, IAggregateRoot, IMustHaveOrg<long>
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; } = string.Empty;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string? UserName { get; set; } = string.Empty;
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTimeOffset LoginTime { get; set; }
        /// <summary>
        /// 登录Ip地址
        /// </summary>
        public string? IpAddress { get; set; }
        /// <summary>
        /// 登录地点
        /// </summary>
        public string? Location { get; set; }
        /// <summary>
        /// 登录途径（平台、小程序、APP）
        /// </summary>
        public string? LoginWay { get; set; }
        /// <summary>
        /// LoginProvider
        /// </summary>
        public string? LoginProvider { get; set; }
        /// <summary>
        /// ProviderKey
        /// </summary>
        public string? ProviderKey { get; set; }
        /// <summary>
        /// ProviderDisplayName
        /// </summary>
        public string? ProviderDisplayName { get; set; }
        /// <summary>
        /// 客户端浏览器
        /// </summary>
        public string? ClientBrowser { get; set; }
        /// <summary>
        /// 客户端系统
        /// </summary>
        public string? ClientOs { get; set; }
        /// <summary>
        /// 登录状态(成功，失败)
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 登录描述
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
