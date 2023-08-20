/**************************************************************
 * 
 * 唯一标识：9671232b-a107-4c15-95e7-90e9fcba7e64
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/7/27 11:54:02
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Domain.Entities
{
    /// <summary>
    /// 实体对象相关领域事件
    /// </summary>
    public abstract class EntityHaveDomainEvent<TPrimaryKey> : IEntityHaveDomainEvent<TPrimaryKey>
    {

        #region IEntity<TPrimaryKey>

        /// <summary>
        /// ID ，实体的唯一标识符
        /// </summary>
        public abstract TPrimaryKey Id { get; set; }
        /// <summary>
        /// 检查此实体是否是暂时的（未持久化到数据库）
        /// </summary>
        /// <returns></returns>
        public abstract bool IsTransient();

        #endregion

        #region IDomainEventManage

        /// <summary>
        /// 领域事件集合 
        /// </summary>
        protected List<INotification>? _domainEvents;


        /// <summary>
        /// 实体对象相关领域事件集合
        /// </summary>
        public virtual IReadOnlyCollection<INotification> DomainEvents //=> _domainEvents?.AsReadOnly();
        {
            get
            {
                _domainEvents ??= new List<INotification>();
                return _domainEvents.AsReadOnly();
            }
        }

        /// <summary>
        /// 实体对象添加领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        public virtual void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }
        /// <summary>
        /// 实体对象移除领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        public virtual void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }
        /// <summary>
        /// 实体对象清空领域事件
        /// </summary>
        public virtual void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        #endregion
    }
}
