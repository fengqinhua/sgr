/**************************************************************
 * 
 * 唯一标识：a168cef5-67dc-4775-944e-96e1718c3318
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/24 16:44:01
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.Exceptions;
using Sgr.UPMS.Domain.Organizations;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Organizations
{
    public class AuthenticationCommandHandle : IRequestHandler<AuthenticationCommand, bool>
    {
        private readonly IOrganizationChecker _organizationChecker;
        private readonly IOrganizationRepository _organizationRepository;

        public AuthenticationCommandHandle(IOrganizationChecker organizationChecker, IOrganizationRepository organizationRepository)
        {
            _organizationChecker = organizationChecker;
            _organizationRepository = organizationRepository;
        }   

        public async Task<bool> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            var org = await _organizationRepository.GetAsync(request.Id, cancellationToken);

            if (org == null)
                return false;

            //附件保存待实现 ... 
            string businessLicensePath = "local#";

            await org.SubmitToConfirmed(request.UsciCode, businessLicensePath, _organizationChecker);

            org.Name = request.Name;
            org.OrgTypeCode = request.OrgTypeCode;
            org.AreaCode = request.AreaCode;
            org.Leader = request.Leader;
            org.Phone = request.Phone;
            org.Email = request.Email;
            org.Address = request.Address;

            await _organizationRepository.UpdateAsync(org, cancellationToken);

            return await _organizationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
