/**************************************************************
 * 
 * 唯一标识：59962f78-7cb8-4682-83f3-27841841654a
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/24 16:37:29
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
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Organizations
{
    public class UpdateorgCommandHandle : IRequestHandler<UpdateOrgCommand, bool>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public UpdateorgCommandHandle(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<bool> Handle(UpdateOrgCommand request, CancellationToken cancellationToken)
        {
            var org = await _organizationRepository.GetAsync(request.Id, cancellationToken);

            if (org == null)
                return false;

            org.Name = request.Name;
            org.StaffSizeCode = request.StaffSizeCode;
            org.Remarks = request.Remarks;

            org.OrgTypeCode = request.OrgTypeCode;
            org.AreaCode = request.AreaCode;
            org.Leader = request.Leader;
            org.Phone = request.Phone;
            org.Email = request.Email;
            org.Address = request.Address;


            await _organizationRepository.UpdateAsync(org, cancellationToken);

            //发布组织机构改变事件
            org.AddDomainEvent(new OrganizationChangedDomainEvent(org.Id));

            return await _organizationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
