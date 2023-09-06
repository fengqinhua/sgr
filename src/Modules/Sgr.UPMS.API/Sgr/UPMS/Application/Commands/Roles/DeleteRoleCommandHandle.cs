/**************************************************************
 * 
 * 唯一标识：8b3e481a-aec6-448e-84dc-3a2e298df0a6
 * 命名空间：Sgr.UPMS.Application.Commands.Roles
 * 创建时间：2023/8/27 17:45:51
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Roles;
using Sgr.UPMS.Events;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Roles
{
    public class DeleteRoleCommandHandle : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandle(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            //角色不存在，删除失败
            var role = await _roleRepository.GetAsync(request.RoleId, cancellationToken);
            if (role == null)
                return false;

            await _roleRepository.DeleteAsync(role, cancellationToken);

            //发布用户改变事件
            role.AddDomainEvent(new RoleChangedDomainEvent(role.Id));

            return await _roleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
