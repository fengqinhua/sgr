/**************************************************************
 * 
 * 唯一标识：833317f8-7b59-43d9-8ab1-1f45eeab43e6
 * 命名空间：Sgr.IntegrationEvents
 * 创建时间：2023/9/5 10:27:02
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.IntegrationEvents
{
    public sealed class InMemoryEventBus
    {
        static InMemoryEventBus()
        {
        }

        private InMemoryEventBus()
        {
            _handlersDictionary = new Dictionary<string, List<IIntegrationEventHandler>>();
        }

        public static InMemoryEventBus Instance { get; } = new InMemoryEventBus();

        private readonly IDictionary<string, List<IIntegrationEventHandler>> _handlersDictionary;

        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {
            var eventType = typeof(T).FullName;
            if (eventType != null)
            {
                if (_handlersDictionary.ContainsKey(eventType))
                {
                    var handlers = _handlersDictionary[eventType];
                    handlers.Add(handler);
                }
                else
                {
                    _handlersDictionary.Add(eventType, new List<IIntegrationEventHandler> { handler });
                }
            }
        }

        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            var eventType = @event.GetType().FullName;

            if (eventType == null)
            {
                return;
            }

            List<IIntegrationEventHandler> integrationEventHandlers = _handlersDictionary[eventType];

            foreach (var integrationEventHandler in integrationEventHandlers)
            {
                if (integrationEventHandler is IIntegrationEventHandler<T> handler)
                {
                    await handler.Handle(@event);
                }
            }
        }
    }

    public class InMemoryEventBusClient : IEventsBus
    {
        private readonly ILogger _logger;

        public InMemoryEventBusClient(ILogger<InMemoryEventBusClient> logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
        }

        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            _logger.LogInformation("Publishing {Event}", @event.GetType().FullName);
            await InMemoryEventBus.Instance.Publish(@event);
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }

        public void StartConsuming()
        {
        }
    }

    public interface IEventsBus : IDisposable
    {
        Task Publish<T>(T @event)
            where T : IntegrationEvent;

        void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent;

        void StartConsuming();
    }

    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }

    public abstract class IntegrationEvent : INotification
    {
        public Guid Id { get; }

        public DateTime OccurredOn { get; }

        protected IntegrationEvent(Guid id, DateTime occurredOn)
        {
            this.Id = id;
            this.OccurredOn = occurredOn;
        }
    }



}
