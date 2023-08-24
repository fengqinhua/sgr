/**************************************************************
 * 
 * 唯一标识：6322bbe9-28e8-44b9-84d6-b9bde1603171
 * 命名空间：Sgr.UPMS.Application.Commands.Organizations
 * 创建时间：2023/8/24 17:26:18
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Organizations;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Organizations
{
    public class CancellationOrgCommandHandle : IRequestHandler<CancellationOrgCommand, bool>
    {
        private readonly IOrganizationManage _organizationManage;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;

        public CancellationOrgCommandHandle(IOrganizationManage organizationManage, IOrganizationRepository organizationRepository, IUserRepository userRepository, ICurrentUser currentUser)
        {
            _organizationManage = organizationManage;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(CancellationOrgCommand request, CancellationToken cancellationToken)
        {
            //组织不存在，注销失败
            var org = await _organizationRepository.GetAsync(request.Id, cancellationToken);
            if (org == null)
                return false;

            //用户不在注销失败
            if (!long.TryParse(_currentUser.Id, out long userId))
                return false;

            var user = await _userRepository.GetAsync(userId, cancellationToken);
            if (user == null)
                return false;

            //用户密码验证失败，注销失败
            if (!user.CheckPassWord(request.Password))
                return false;

            //组织前审查
            if(!await  _organizationManage.CancellationExamination(org, user))
                return false;

            //准备执行删除
            //添加删除事件

            await _organizationRepository.DeleteAsync(org, cancellationToken); 


            return await _organizationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
