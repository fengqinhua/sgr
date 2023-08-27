/**************************************************************
 * 
 * 唯一标识：36bb593a-fc93-4a45-93f4-7c41318bd4bd
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/27 11:34:55
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Application.Commands.Organizations;
using Sgr.UPMS.Domain.Departments;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Infrastructure.Repositories;
using Sgr.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Users
{
    public class CreateUserCommandHandle : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserChecker _userChecker;
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public CreateUserCommandHandle(IUserChecker userChecker, IUserRepository userRepository, IDepartmentRepository departmentRepository)
        {
            _userChecker = userChecker;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //创建User
            User user = await User.CreateNewAsync(request.LoginName, HashHelper.CreateMd5(request.LoginPassword), request.OrgId, _userChecker);
            user.UserName = request.UserName;

            //绑定所属部门
            if (request.DepartmentId.HasValue)
            {
                Department? department = await _departmentRepository.GetAsync(request.DepartmentId.Value, cancellationToken);
                if(department != null)
                    user.BindDepartment(department.Id, department.Name);
            }

            //绑定所属岗位
            if (request.DutyIds != null)
                user.BindDuties(request.DutyIds!);

            //绑定所属角色
            if (request.RoleIds != null)
                user.BindRoles(request.RoleIds!);

            await _userRepository.InsertAsync(user, cancellationToken);

            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

}
