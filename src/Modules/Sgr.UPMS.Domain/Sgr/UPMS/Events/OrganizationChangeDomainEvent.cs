/**************************************************************
 * 
 * 唯一标识：6c5b568a-01cf-4d20-9c87-d9067b111239
 * 命名空间：Sgr.UPMS.Application.DomainEventHandlers
 * 创建时间：2023/9/4 6:34:01
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;

namespace Sgr.UPMS.Application.DomainEventHandlers
{
    /// <summary>
    ///  组织机构信息改变
    /// </summary>
    public class OrganizationChangeDomainEvent : INotification
    {
        public long OrgId { get; }

        public OrganizationChangeDomainEvent(long orgId)
        {
            this.OrgId = orgId;
        }
    }
}
