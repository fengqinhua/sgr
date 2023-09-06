/**************************************************************
 * 
 * 唯一标识：2c2f0ada-504a-4ef7-b257-8983c5d94a7d
 * 命名空间：Sgr.UPMS.Application.Commands.Roles
 * 创建时间：2023/8/27 17:50:46
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Roles;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Roles
{
    public class AllocateFunctionPermissionCommandHandle : IRequestHandler<AllocateFunctionPermissionCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public AllocateFunctionPermissionCommandHandle(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(AllocateFunctionPermissionCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetAsync(request.RoleId, cancellationToken);

            if (role == null)
                return false;

            role.ResetPermission(ResourceType.FunctionalPermission);

            if(request.FunctionPermissions != null)
            {
                foreach(var permission in request.FunctionPermissions)
                {
                    role.AddPermission(ResourceType.FunctionalPermission, permission);
                }
            }

            await _roleRepository.UpdateAsync(role, cancellationToken);

            //发布用户改变事件
            role.AddDomainEvent(new RoleChangedDomainEvent(role.Id));

            return await _roleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
