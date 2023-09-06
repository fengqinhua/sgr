/**************************************************************
 * 
 * 唯一标识：ad597c0f-3250-4067-890c-51858d5aa2e8
 * 命名空间：Sgr.UPMS.Application.DomainEventHandlers
 * 创建时间：2023/9/6 7:41:48
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.Caching.Services;
using Sgr.Domain.Uow;
using System.Threading.Tasks;
using System.Threading;
using Sgr.UPMS.Events;

namespace Sgr.UPMS.Application.DomainEventHandlers
{
    public class RoleChangedDomainEventHandle : INotificationHandler<RoleChangedDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheManager _cacheManager;

        public RoleChangedDomainEventHandle(
            IUnitOfWork unitOfWork,
            ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RoleChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            Task clearCacheAsync()
            {
                return _cacheManager.RemoveAsync(string.Format(CacheKeys.ROLE_KEY_WITH_RESOURCES, notification.RoleId));
            };

            if (_unitOfWork.HasActiveTransaction)
                _unitOfWork.AddTransactionCompletedHandler(clearCacheAsync);
            else
                await clearCacheAsync();

        }
    }
}
