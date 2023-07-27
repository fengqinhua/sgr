/**************************************************************
 * 
 * 唯一标识：a6bfd8ae-9940-4669-bed8-f939c7a86ca9
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/7/27 14:11:01
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
    /// 实体对象领域事件管理
    /// </summary>
    public interface IDomainEventManage
    {
        /// <summary>
        /// 实体对象相关领域事件集合
        /// </summary>
        IReadOnlyCollection<INotification> DomainEvents { get; }
        /// <summary>
        /// 实体对象添加领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        void AddDomainEvent(INotification eventItem);
        /// <summary>
        /// 实体对象移除领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        void RemoveDomainEvent(INotification eventItem);
        /// <summary>
        /// 实体对象清空领域事件
        /// </summary>
        void ClearDomainEvents();
    }
}
