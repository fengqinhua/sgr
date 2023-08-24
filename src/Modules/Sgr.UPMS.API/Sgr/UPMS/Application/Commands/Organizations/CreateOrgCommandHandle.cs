/**************************************************************
 * 
 * 唯一标识：237f8131-b7cb-404c-adde-a026a77f10cb
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/24 6:30:56
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Organizations;
using Sgr.UPMS.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Organizations
{
    public class CreateOrgCommandHandle : IRequestHandler<CreateOrgCommand, bool>
    {
        private readonly IOrganizationManage _organizationManage;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserChecker _userChecker;
        private readonly IUserRepository _userRepository;

        public CreateOrgCommandHandle(IOrganizationManage organizationManage, IOrganizationRepository organizationRepository, IUserChecker userChecker, IUserRepository userRepository)
        {
            _organizationManage = organizationManage;
            _organizationRepository = organizationRepository;
            _userChecker = userChecker;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(CreateOrgCommand request, CancellationToken cancellationToken)
        {
            Organization organization = await _organizationManage.CreateNewAsync(request.Name,
                request.OrgTypeCode,
                request.AreaCode,
                request.StaffSizeCode,
                request.Leader,
                request.Phone,
                request.Email,
                request.Address,
                request.OrderNumber,
                request.Remarks,
                request.ParentId);

            User user = await User.CreateNewAsync(request.AdminName, request.AdminPassword, organization.Id, _userChecker);

            await _organizationRepository.InsertAsync(organization, cancellationToken);
            await _userRepository.InsertAsync(user, cancellationToken);

            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
