/**************************************************************
 * 
 * 唯一标识：1e2006ac-b3bf-4205-9bb3-828a49a8de1b
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/27 12:40:57
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
    public class ModifyPasswordCommandHandle : IRequestHandler<ModifyPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ModifyPasswordCommandHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ModifyPasswordCommand request, CancellationToken cancellationToken)
        {
            //用户不存在，修改密码失败
            var user = await _userRepository.GetAsync(request.UserId, cancellationToken);
            if (user == null)
                return false;

            //旧密码验证失败，修改密码失败
            if (!user.CheckPassWord(request.OldPassword))
                return false;

            user.ChangePassword(HashHelper.CreateMd5(request.NewPassword));
            await _userRepository.UpdateAsync(user, cancellationToken);

            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
