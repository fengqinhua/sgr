/**************************************************************
 * 
 * 唯一标识：038aacae-2822-4a80-9ce9-9b6a22985d13
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/24 17:21:04
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Sgr.Oss.Services;
using Sgr.UPMS.Application.DomainEventHandlers;
using Sgr.UPMS.Domain.Organizations;
using Sgr.Utilities;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Organizations
{
    public class ModifyOrgLogoCommandHandle : IRequestHandler<ModifyOrgLogoCommand, bool>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public ModifyOrgLogoCommandHandle(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<bool> Handle(ModifyOrgLogoCommand request, CancellationToken cancellationToken)
        {
            var org = await _organizationRepository.GetAsync(request.Id, cancellationToken);

            if (org == null)
                return false;

            org.LogoUrl = request.LogoObjectName;
            await _organizationRepository.UpdateAsync(org, cancellationToken);

            //发布组织机构改变事件
            org.AddDomainEvent(new OrganizationChangedDomainEvent(org.Id));

            return await _organizationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
