/**************************************************************
 * 
 * 唯一标识：40f79274-42c4-4067-97be-63864274b2ed
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/24 16:14:30
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Application.DomainEventHandlers;
using Sgr.UPMS.Domain.Organizations;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Events;
using Sgr.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Organizations
{
    public class RegisterOrgCommandHadle : IRequestHandler<RegisterOrgCommand, bool>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserChecker _userChecker;
        private readonly IUserRepository _userRepository;

        public RegisterOrgCommandHadle(IOrganizationRepository organizationRepository, IUserChecker userChecker, IUserRepository userRepository)
        {
            _organizationRepository = organizationRepository;
            _userChecker = userChecker;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(RegisterOrgCommand request, CancellationToken cancellationToken)
        {
            Organization organization = new Organization(request.Name, request.StaffSizeCode, request.Remarks);

            User user = await User.CreateNewAsync(request.AdminName, HashHelper.CreateMd5(request.AdminPassword), -1, _userChecker);
            user.UserPhone = request.Phone;
            user.UserEmail = request.Email;

            await _organizationRepository.InsertAsync(organization, cancellationToken);
            await _userRepository.InsertAsync(user, cancellationToken);

            //发布组织机构改变事件
            organization.AddDomainEvent(new OrganizationChangedDomainEvent(organization.Id));
            //发布用户改变事件
            user.AddDomainEvent(new UserChangedDomainEvent(user.Id));

            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
