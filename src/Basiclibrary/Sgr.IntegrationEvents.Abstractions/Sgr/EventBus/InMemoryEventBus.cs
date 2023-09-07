/**************************************************************
 * 
 * 唯一标识：a1111317-31c9-48d1-b0e5-cb243063d7d1
 * 命名空间：Sgr.EventBus
 * 创建时间：2023/9/7 12:04:17
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EventBus
{
    public class InMemoryEventBus : IEventsBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventBusSubscriptionsManager _eventBusSubscriptionsManager;

        public InMemoryEventBus(
            IEventBusSubscriptionsManager eventBusSubscriptionsManager,
            IServiceProvider serviceProvider)
        {
            _eventBusSubscriptionsManager = eventBusSubscriptionsManager;
            _serviceProvider = serviceProvider;
        }


        #region IEventsBus

        public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
        {
            var subscriptionInfos = _eventBusSubscriptionsManager.GetHandlersForEvent<T>();
            if (subscriptionInfos == null)
                return;

            await using var scope = _serviceProvider.CreateAsyncScope();

            foreach (var subscriptionInfo in subscriptionInfos)
            {
                var handler = scope.ServiceProvider.GetService(subscriptionInfo.HandlerType);
                if (handler == null) continue;

                MethodInfo methodInfo = subscriptionInfo.HandlerType.GetMethod("HandleAsync");
                if (methodInfo == null) continue;

                await (Task)methodInfo.Invoke(handler, new[] { @event });
            }
        }


        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            _eventBusSubscriptionsManager.AddSubscription<T, TH>();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            _eventBusSubscriptionsManager.RemoveSubscription<T, TH>();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            
        }

        #endregion

    }
}
