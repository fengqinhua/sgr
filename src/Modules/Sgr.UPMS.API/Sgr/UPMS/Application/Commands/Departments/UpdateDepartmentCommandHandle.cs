/**************************************************************
 * 
 * 唯一标识：63964459-07af-4c3a-a43b-2bd80ebec916
 * 命名空间：Sgr.UPMS.Application.Commands.Departments
 * 创建时间：2023/8/27 20:00:00
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
    public class UpdateDepartmentCommandHandle : IRequestHandler<UpdateDepartmentCommand, bool>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public UpdateDepartmentCommandHandle(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }


        public async Task<bool> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetAsync(request.DepartmentId, cancellationToken);

            if (department == null)
                return false;

            department.ChangeName(request.Name);
            department.Remarks = request.Remarks;
            department.OrderNumber = request.OrderNumber;

            department.Leader = request.Leader;
            department.Phone = request.Phone;
            department.Email = request.Email;

            await _departmentRepository.UpdateAsync(department, cancellationToken);

            return await _departmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        }
    }
}
