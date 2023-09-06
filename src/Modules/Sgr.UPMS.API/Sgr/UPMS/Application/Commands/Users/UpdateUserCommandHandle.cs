/**************************************************************
 * 
 * 唯一标识：548b3d37-371e-4b0a-b726-20a58737ee3c
 * 命名空间：Sgr.UPMS.Application.Commands.Users
 * 创建时间：2023/8/27 11:46:15
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Departments;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Users
{
    public class UpdateUserCommandHandle : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public UpdateUserCommandHandle(IUserRepository userRepository, IDepartmentRepository departmentRepository)
        {
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId, cancellationToken);

            if (user == null)
                return false;

            user.UserName = request.UserName;

            //绑定所属部门
            if (request.DepartmentId.HasValue)
            {
                Department? department = await _departmentRepository.GetAsync(request.DepartmentId.Value, cancellationToken);
                if (department != null)
                    user.BindDepartment(department.Id, department.Name);
            }

            //绑定所属岗位
            if (request.DutyIds != null)
                user.BindDuties(request.DutyIds!);

            //绑定所属角色
            if (request.RoleIds != null)
                user.BindRoles(request.RoleIds!);

            await _userRepository.UpdateAsync(user, cancellationToken);

            //发布用户改变事件
            user.AddDomainEvent(new UserChangedDomainEvent(user.Id));

            return await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);



        }
    }
}
