/**************************************************************
 * 
 * 唯一标识：410765c8-463b-47e3-912c-acd0d4f35e85
 * 命名空间：Sgr.UPMS.Application.Commands.Roles
 * 创建时间：2023/8/27 17:41:28
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
    public class UpdateRoleCommandHandle : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleCommandHandle(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetAsync(request.RoleId, cancellationToken);

            if (role == null)
                return false;

            role.RoleName = request.RoleName;
            role.Remarks = request.Remarks;
            role.OrderNumber = request.OrderNumber;

            role.ChangeEntityStates(request.State);

            await _roleRepository.UpdateAsync(role, cancellationToken);

            //发布用户改变事件
            role.AddDomainEvent(new RoleChangedDomainEvent(role.Id));

            return await _roleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

    }
}
