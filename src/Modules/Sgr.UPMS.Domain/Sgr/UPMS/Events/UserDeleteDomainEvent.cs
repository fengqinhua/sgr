/**************************************************************
 * 
 * 唯一标识：001fa77b-82d4-4d7a-881f-b0a790eb0638
 * 命名空间：Sgr.UPMS.Events
 * 创建时间：2023/8/27 11:55:11
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
    /// <summary>
    /// 用户删除事件
    /// </summary>
    public class UserDeleteDomainEvent : INotification
    {
        public long UserId { get; }

        public UserDeleteDomainEvent(long userId)
        {
            this.UserId = userId;
        }
    }
}
