/**************************************************************
 * 
 * 唯一标识：1216e3ca-e226-48d0-9e56-72f98ed8a694
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/27 11:49:10
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Users
{
    public class DeleteUserCommandHandle : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;

        public DeleteUserCommandHandle(IUserRepository userRepository, ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //用户不存在，删除失败
            var user = await _userRepository.GetAsync(request.UserId, cancellationToken);
            if (user == null)
                return false;

            //当前登录用户不存在，删除失败
            if (!long.TryParse(_currentUser.Id, out long currentUserId))
                return false;

            var current_user = await _userRepository.GetAsync(currentUserId, cancellationToken);
            if (current_user == null)
                return false;

            //当前登录密码验证失败，删除失败
            if (!current_user.CheckPassWord(request.Password))
                return false;

            //执行删除
            await _userRepository.DeleteAsync(user, cancellationToken);

            //发布用户改变事件
            user.AddDomainEvent(new UserChangedDomainEvent(user.Id));

            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
