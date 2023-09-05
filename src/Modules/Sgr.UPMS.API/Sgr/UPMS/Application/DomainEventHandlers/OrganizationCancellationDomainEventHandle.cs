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

namespace Sgr.UPMS.Application.DomainEventHandlers
{
    public class OrganizationCancellationDomainEventHandle : INotificationHandler<OrganizationCancellationDomainEvent>
    {
        //private readonly IUserRepository _userRepository;
        //private readonly ILogLoginRepository _logLoginRepository;
        //private readonly IDutyRepository _dutyRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger _logger;

        public OrganizationCancellationDomainEventHandle(
            //IUserRepository userRepository,
            //ILogLoginRepository logLoginRepository,
            //IDutyRepository dutyRepository,
            //IDepartmentRepository departmentRepository,

            ILogger<OrganizationCancellationDomainEventHandle> logger)
        {
            //_userRepository = userRepository;
            //_logLoginRepository = logLoginRepository;
            //_dutyRepository = dutyRepository;
            //_departmentRepository = departmentRepository;

            _logger = logger;
        }

        public async Task Handle(OrganizationCancellationDomainEvent notification, CancellationToken cancellationToken)
        {
            //await _userRepository.DeleteByOrgIdAsync(notification.OrgId, cancellationToken);
            //    await _logLoginRepository.DeleteByOrgIdAsync(notification.OrgId, cancellationToken);
            //    await _dutyRepository.DeleteByOrgIdAsync(notification.OrgId, cancellationToken);
            //    await _departmentRepository.DeleteByOrgIdAsync(notification.OrgId, cancellationToken);



            //await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            _logger.LogWarning($"Organization (id = {notification.OrgId}) Cancellation Domain Event Handled");

        }
    }
}
