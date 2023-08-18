/**************************************************************
 * 
 * 唯一标识：a2b556b6-3449-47d2-b186-951daa97d239
 * 命名空间：Sgr.Application.IntegrationEvents
 * 创建时间：2023/8/18 11:09:15
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Application.IntegrationEvents
{
    public class DefaultIntegrationEventService : IIntegrationEventService
    {
        public Task AddAndSaveEventAsync(IntegrationEvent evt)
        {
            return Task.CompletedTask;
        }

        public Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            return Task.CompletedTask;
        }
    }
}
