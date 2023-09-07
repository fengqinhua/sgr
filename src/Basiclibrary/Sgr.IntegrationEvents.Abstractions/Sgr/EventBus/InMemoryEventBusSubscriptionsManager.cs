/**************************************************************
 * 
 * 唯一标识：58e45ff7-6720-4fd1-8488-3e085bfd1cdb
 * 命名空间：Sgr.EventBus
 * 创建时间：2023/9/7 11:36:11
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Sgr.EventBus
{
    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string>? OnEventRemoved;

        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }

        public bool IsEmpty => _handlers is { Count: 0 };
        public void Clear() => _handlers.Clear();


        public void AddSubscription<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();

            DoAddSubscription(typeof(THandler), eventName);

            if (!_eventTypes.Contains(typeof(TEvent)))
            {
                _eventTypes.Add(typeof(TEvent));
            }
        }

        public string GetEventKey<TEvent>()
        {
            return typeof(TEvent).Name;
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>() where TEvent : IntegrationEvent
        {
            var key = GetEventKey<TEvent>();
            return GetHandlersForEvent(key);
        }

        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);


        public bool HasSubscriptionsForEvent<TEvent>() where TEvent : IntegrationEvent
        {
            var key = GetEventKey<TEvent>();
            return HasSubscriptionsForEvent(key);
        }


        public void RemoveSubscription<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>
        {
            var handlerToRemove = FindSubscriptionToRemove<TEvent, THandler>();
            var eventName = GetEventKey<TEvent>();
            DoRemoveHandler(eventName, handlerToRemove);
        }


        private void DoAddSubscription(Type handlerType, string eventName)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
        }

        private SubscriptionInfo? FindSubscriptionToRemove<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();
            return DoFindSubscriptionToRemove(eventName, typeof(THandler));
        }

        private SubscriptionInfo? DoFindSubscriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
                return null;

            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }

        private void DoRemoveHandler(string eventName, SubscriptionInfo? subsToRemove)
        {
            if (subsToRemove != null)
            {
                _handlers[eventName].Remove(subsToRemove);
                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType != null)
                    {
                        _eventTypes.Remove(eventType);
                    }
                    RaiseOnEventRemoved(eventName);
                }
            }
        }

        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }


    }
}
