/**************************************************************
 * 
 * 唯一标识：6df57dd7-9df2-41dc-b770-ac2bb434bda5
 * 命名空间：Sgr.IntegrationEvents
 * 创建时间：2023/8/19 17:11:40
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.EventBus;
using System;
using System.Threading.Tasks;

namespace Sgr.IntegrationEvents
{
    public interface IIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
