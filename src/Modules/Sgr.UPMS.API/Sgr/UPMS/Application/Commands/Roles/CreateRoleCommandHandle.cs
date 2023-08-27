/**************************************************************
 * 
 * 唯一标识：c8efb793-8f6f-48bb-a575-4710a3afc676
 * 命名空间：Sgr.UPMS.Application.Commands.Roles
 * 创建时间：2023/8/27 17:35:17
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Application.Commands.Users;
using Sgr.UPMS.Domain.Roles;
using Sgr.UPMS.Domain.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Roles
{
    public class CreateRoleCommandHandle : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleCommandHandle(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = new Role(Guid.NewGuid().ToString("N"),
                request.RoleName,
                request.OrgId ?? Constant.DEFAULT_ORGID,
                request.OrderNumber,
                request.Remarks);

            await _roleRepository.InsertAsync(role, cancellationToken);

            return await _roleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
