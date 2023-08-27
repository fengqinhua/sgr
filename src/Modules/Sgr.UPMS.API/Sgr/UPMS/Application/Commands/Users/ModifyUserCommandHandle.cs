/**************************************************************
 * 
 * 唯一标识：6efab944-b48c-4979-b670-6494f42d3c14
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/27 12:47:15
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Users
{
    public class ModifyUserCommandHandle : IRequestHandler<ModifyUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ModifyUserCommandHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ModifyUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId, cancellationToken);

            if (user == null)
                return false;

            user.UserName = request.UserName;
            user.UserPhone = request.UserPhone;
            user.UserEmail = request.UserEmail;
            user.QQ = request.QQ;
            user.Wechat = request.Wechat;

            await _userRepository.UpdateAsync(user, cancellationToken);

            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
