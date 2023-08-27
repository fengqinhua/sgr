/**************************************************************
 * 
 * 唯一标识：ca6cca04-784c-4b97-b722-316207a5c9d5
 * 命名空间：Sgr.UPMS.Application.Commands.Departments
 * 创建时间：2023/8/27 19:35:22
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Departments;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Departments
{
    public class CreateDepartmentCommandHandle : IRequestHandler<CreateDepartmentCommand, bool>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentManage _departmentManage;

        public CreateDepartmentCommandHandle(IDepartmentRepository departmentRepository, IDepartmentManage departmentManage)
        {
            _departmentRepository = departmentRepository;
            _departmentManage = departmentManage;
        }

        public async Task<bool> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Department department = await _departmentManage.CreateNewAsync(request.Name, request.OrderNumber, request.Remarks, request.Leader, request.Phone, request.Email, request.OrgId, request.ParentId);
            await _departmentRepository.InsertAsync(department, cancellationToken);
            return await _departmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
