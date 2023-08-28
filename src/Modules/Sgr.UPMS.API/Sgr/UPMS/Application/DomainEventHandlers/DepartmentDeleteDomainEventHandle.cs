/**************************************************************
 * 
 * 唯一标识：855e5896-8fcf-4945-979d-37d38d51b513
 * 命名空间：Sgr.UPMS.Application.DomainEventHandlers
 * 创建时间：2023/8/27 20:23:24
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
    public class DepartmentDeleteDomainEventHandle : INotificationHandler<DepartmentDeleteDomainEvent>
    {
        private readonly SgrDbContext _sgrDbContext;

        public DepartmentDeleteDomainEventHandle(SgrDbContext sgrDbContext)
        {
            _sgrDbContext = sgrDbContext;
        }

        public async Task Handle(DepartmentDeleteDomainEvent notification, CancellationToken cancellationToken)
        {
            var users = await _sgrDbContext
                .Set<User>()
                .AsTracking()
                .Where(f => f.DepartmentId == notification.DepartmentId)
                .ToListAsync(cancellationToken);

            foreach (var user in users)
            {
                user.BindDepartment(0, "");
            }

            await _sgrDbContext.SaveEntitiesAsync(cancellationToken);
        }
    }
}
