/**************************************************************
 * 
 * 唯一标识：47f83776-020d-440d-9339-1ebba8ec84ab
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/24 17:08:41
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Organizations;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Organizations
{
    public class AssociatedParentOrgCommandHandle : IRequestHandler<AssociatedParentOrgCommand, bool>
    {
        private readonly IOrganizationManage _organizationManage;
        private readonly IOrganizationRepository _organizationRepository;

        public AssociatedParentOrgCommandHandle(IOrganizationManage organizationManage, IOrganizationRepository organizationRepository)
        {
            _organizationManage = organizationManage;
            _organizationRepository = organizationRepository;
        }

        public async Task<bool> Handle(AssociatedParentOrgCommand request, CancellationToken cancellationToken)
        {
            var org = await _organizationRepository.GetAsync(request.Id, cancellationToken);

            if (org == null)
                return false;

            await _organizationManage.ChangeParentIdAsync(org, request.ParentId ?? 0, 5, cancellationToken);

            return true;
        }
    }
}
