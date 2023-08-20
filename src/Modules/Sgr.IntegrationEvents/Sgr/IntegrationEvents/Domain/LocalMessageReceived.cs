/**************************************************************
 * 
 * 唯一标识：e9875664-57fa-4abc-af7d-698a9301edff
 * 命名空间：Sgr.IntegrationEvents.Domain
 * 创建时间：2023/8/19 21:44:57
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;
using System;

namespace Sgr.IntegrationEvents.Domain
{
    public class LocalMessageReceived : Entity<long>, IOptimisticLock
    {
        /// <summary>
        /// 消息名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 消息分类
        /// </summary>
        public string? Group { get; set; }
        /// <summary>
        /// 转发次数
        /// </summary>
        public int Retries { get; set; } = 0;
        /// <summary>
        /// 消息创建时间
        /// </summary>
        public DateTimeOffset CreationTime { get; set; }
        /// <summary>
        /// 消息失效时间
        /// </summary>
        public DateTimeOffset ExpirationTime { get; set; }
        /// <summary>
        /// 消息状态
        /// </summary>
        public MessageState State { get; set; }

        #region IOptimisticLock (乐观锁)

        /// <summary>
        /// 行版本
        /// </summary>
        public long RowVersion { get; set; }

        #endregion
    }
}
