/**************************************************************
 * 
 * 唯一标识：91753bd0-6c72-44a6-92cd-7f5c58335d1c
 * 命名空间：Sgr.UPMS.Application.Commands.Roles
 * 创建时间：2023/8/27 17:48:13
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Roles;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Roles
{
    public class ModifyRoleStatusCommandHandle : IRequestHandler<ModifyRoleStatusCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public ModifyRoleStatusCommandHandle(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(ModifyRoleStatusCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetAsync(request.RoleId, cancellationToken);

            if (role == null)
                return false;

            role.ChangeEntityStates(request.State);

            await _roleRepository.UpdateAsync(role, cancellationToken);

            return await _roleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
