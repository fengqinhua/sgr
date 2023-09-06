/**************************************************************
 * 
 * 唯一标识：3fedb409-9b61-4bdc-a54c-3bcf4e430444
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/27 12:49:36
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Application.Commands.Roles;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Users
{
    public class ModifyUserStatusCommandHandle : IRequestHandler<ModifyUserStatusCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ModifyUserStatusCommandHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ModifyUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId, cancellationToken);

            if (user == null)
                return false;

            user.ChangeEntityStates(request.State);

            await _userRepository.UpdateAsync(user, cancellationToken);

            //发布用户改变事件
            user.AddDomainEvent(new UserChangedDomainEvent(user.Id));

            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
