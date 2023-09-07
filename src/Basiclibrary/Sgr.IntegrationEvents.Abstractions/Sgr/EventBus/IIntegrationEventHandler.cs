/**************************************************************
 * 
 * 唯一标识：73c66378-6d3a-4f50-b250-bd8e7e730d6e
 * 命名空间：Sgr.IntegrationEvents
 * 创建时间：2023/9/7 11:22:40
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

namespace Sgr.EventBus
{
    public interface IIntegrationEventHandler
    {
    }

    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent @event);
    }
}
