/**************************************************************
 * 
 * 唯一标识：7d80b942-6120-465b-a05e-38987816b12d
 * 命名空间：Sgr.UPMS.Events
 * 创建时间：2023/8/26 10:18:14
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
    /// 组织机构注销事件
    /// </summary>
    public class OrganizationCancellationDomainEvent : INotification
    {
        public long OrgId { get; }

        public OrganizationCancellationDomainEvent(long OrgId)
        {
            this.OrgId = OrgId;
        }
    }
}
