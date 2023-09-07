/**************************************************************
 * 
 * 唯一标识：8ca150ab-84c1-4d0f-9c9d-fdb3c8993e77
 * 命名空间：Sgr.IntegrationEvents
 * 创建时间：2023/8/19 17:11:19
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

namespace Sgr.EventBus
{
    public abstract class IntegrationEvent : INotification
    {
        public Guid Id { get; }

        public DateTimeOffset OccurredOn { get; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTimeOffset.UtcNow;
        }

        protected IntegrationEvent(Guid id, DateTimeOffset occurredOn)
        {
            Id = id;
            OccurredOn = occurredOn;
        }
    }
}
