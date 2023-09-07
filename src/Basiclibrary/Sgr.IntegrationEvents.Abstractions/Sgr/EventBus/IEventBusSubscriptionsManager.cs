/**************************************************************
 * 
 * 唯一标识：75d92d14-a3f9-4018-8511-fcbac3dc6753
 * 命名空间：Sgr.EventBus
 * 创建时间：2023/9/7 11:33:21
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.EventBus
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>;

        void RemoveSubscription<TEvent, THandler>()
                where THandler : IIntegrationEventHandler<TEvent>
                where TEvent : IntegrationEvent;

        bool HasSubscriptionsForEvent<TEvent>() where TEvent : IntegrationEvent;
        bool HasSubscriptionsForEvent(string eventName);

        Type GetEventTypeByName(string eventName);
        void Clear();

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>() where TEvent : IntegrationEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        string GetEventKey<TEvent>();
    }
}
