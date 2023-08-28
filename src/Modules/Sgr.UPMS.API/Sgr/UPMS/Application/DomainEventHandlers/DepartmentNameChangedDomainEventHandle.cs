/**************************************************************
 * 
 * 唯一标识：ce93983f-3172-4b80-acb8-ab6ffc5a2b23
 * 命名空间：Sgr.UPMS.Application.DomainEventHandlers
 * 创建时间：2023/8/27 20:29:00
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgr.EntityFrameworkCore;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Events;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.DomainEventHandlers
{
    public class DepartmentNameChangedDomainEventHandle : INotificationHandler<DepartmentNameChangedDomainEvent>
    {
        private readonly SgrDbContext _sgrDbContext;

        public DepartmentNameChangedDomainEventHandle(SgrDbContext sgrDbContext)
        {
            _sgrDbContext = sgrDbContext;
        }

        public async Task Handle(DepartmentNameChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var users = await _sgrDbContext
                .Set<User>()
                .AsTracking()
                .Where(f => f.DepartmentId == notification.DepartmentId)
                .ToListAsync(cancellationToken);

            foreach (var user in users)
            {
                user.BindDepartment(notification.DepartmentId, notification.DepartmentName);
            }

            await _sgrDbContext.SaveEntitiesAsync(cancellationToken);
        }
    }
}
