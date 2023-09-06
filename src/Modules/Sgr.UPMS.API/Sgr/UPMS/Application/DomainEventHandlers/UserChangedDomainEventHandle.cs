/**************************************************************
 * 
 * 唯一标识：0d2b5d70-a489-4f06-9a85-ba209cb8d0b0
 * 命名空间：Sgr.UPMS.Application.DomainEventHandlers
 * 创建时间：2023/9/6 7:43:25
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.Caching.Services;
using Sgr.Domain.Uow;
using Sgr.UPMS.Events;
using System.Threading.Tasks;
using System.Threading;

namespace Sgr.UPMS.Application.DomainEventHandlers
{
    public class UserChangedDomainEventHandle : INotificationHandler<UserChangedDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheManager _cacheManager;

        public UserChangedDomainEventHandle(
            IUnitOfWork unitOfWork,
            ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UserChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            Task clearCacheAsync()
            {
                return _cacheManager.RemoveAsync(string.Format(CacheKeys.USER_KEY_WITH_ROLES, notification.UserId));
            };

            if (_unitOfWork.HasActiveTransaction)
                _unitOfWork.AddTransactionCompletedHandler(clearCacheAsync);
            else
                await clearCacheAsync();

        }
    }
}
