/**************************************************************
 * 
 * 唯一标识：84940502-a594-480c-ac96-4f1634c85183
 * 命名空间：Sgr.UPMS.Application.DomainEventHandlers
 * 创建时间：2023/8/27 11:56:18
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.Extensions.Logging;
using Sgr.UPMS.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.DomainEventHandlers
{
    public class UserDeleteDomainEventHandle : INotificationHandler<UserDeleteDomainEvent>
    {
        private readonly ILogger _logger;

        public UserDeleteDomainEventHandle(ILogger<UserDeleteDomainEventHandle> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserDeleteDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"User (id = {notification.UserId}) Delete Domain Event Handled");
            return Task.CompletedTask;
        }
    }
}
