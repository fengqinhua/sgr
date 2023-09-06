/**************************************************************
 * 
 * 唯一标识：9176e50e-a06a-4275-80ba-6e62d389b019
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/27 12:45:31
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Events;
using Sgr.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Users
{
    public class ResetPasswordCommandHandle : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ResetPasswordCommandHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async  Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            //用户不存在，重置密码失败
            var user = await _userRepository.GetAsync(request.UserId, cancellationToken);
            if (user == null)
                return false;

            user.ChangePassword(HashHelper.CreateMd5(request.Password));
            await _userRepository.UpdateAsync(user, cancellationToken);

            //发布用户改变事件
            user.AddDomainEvent(new UserChangedDomainEvent(user.Id));

            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
