/**************************************************************
 * 
 * 唯一标识：17fce0c7-0871-424a-9ecf-607ca9a6c628
 * 命名空间：Sgr.IntegrationEvents
 * 创建时间：2023/9/7 11:18:21
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Threading.Tasks;

namespace Sgr.EventBus
{
    public interface IEventsBus : IDisposable
    {
        Task PublishAsync<T>(T @event)
            where T : IntegrationEvent;

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    }
}
