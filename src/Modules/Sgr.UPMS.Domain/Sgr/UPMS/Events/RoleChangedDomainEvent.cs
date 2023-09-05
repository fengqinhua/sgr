/**************************************************************
 * 
 * 唯一标识：82f92edf-12a1-4655-9a8b-3df2cd3edfd6
 * 命名空间：Sgr.UPMS.Events
 * 创建时间：2023/9/4 7:03:35
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

namespace Sgr.UPMS.Events
{
    internal class RoleChangedDomainEvent : INotification
    {
        public long RoleId { get; }

        public RoleChangedDomainEvent(long roleId)
        {
            this.RoleId = roleId;
        }
    }
}