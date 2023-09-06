/**************************************************************
 * 
 * 唯一标识：2bbbf485-1885-4533-89e9-c712f6f5aea2
 * 命名空间：Sgr.UPMS.Application.DomainEventHandlers
 * 创建时间：2023/8/26 10:22:15
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.Extensions.Logging;
using Sgr.UPMS.Domain.Departments;
using Sgr.UPMS.Domain.Duties;
using Sgr.UPMS.Domain.LogLogins;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Events;
using System.Threading;
using System.Threading.Tasks;
using Sgr.Domain.Repositories;
using Sgr.EntityFrameworkCore;
using Sgr.Caching.Services;
using Sgr.Domain.Uow;

namespace Sgr.UPMS.Application.DomainEventHandlers
{
    public class OrganizationChangedDomainEventHandle : INotificationHandler<OrganizationChangedDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheManager _cacheManager;

        public OrganizationChangedDomainEventHandle(
            IUnitOfWork unitOfWork,
            ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(OrganizationChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            Task clearCacheAsync()
            {
                return _cacheManager.RemoveAsync(string.Format(CacheKeys.ORG_KEY, notification.OrgId));
            };

            if (_unitOfWork.HasActiveTransaction)
                _unitOfWork.AddTransactionCompletedHandler(clearCacheAsync);
            else
                await clearCacheAsync();

        }
    }
}
