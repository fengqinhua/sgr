/**************************************************************
 * 
 * 唯一标识：15c4e544-e48c-416a-b43b-6b34cf028a2f
 * 命名空间：Sgr.EventBus
 * 创建时间：2023/9/7 11:34:37
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.EventBus
{
    public class SubscriptionInfo
    {
        public Type HandlerType { get; }

        private SubscriptionInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }

        public static SubscriptionInfo Typed(Type handlerType) =>
            new SubscriptionInfo(handlerType);
    }
}
