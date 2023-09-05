/**************************************************************
 * 
 * 唯一标识：cf7bfdc0-67a4-4806-9ae8-cc6dbe910501
 * 命名空间：Sgr.UPMS.Events
 * 创建时间：2023/9/4 7:02:48
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
    public class UserChangedDomainEvent : INotification
    {
        public long UserId { get; }

        public UserChangedDomainEvent(long userId)
        {
            this.UserId = userId;
        }
    }
}
