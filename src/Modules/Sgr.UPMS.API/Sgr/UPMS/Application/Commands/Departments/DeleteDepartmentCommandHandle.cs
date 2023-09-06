/**************************************************************
 * 
 * 唯一标识：02ba8fb0-777d-481f-af91-72656c4d6f46
 * 命名空间：Sgr.UPMS.Application.Commands.Departments
 * 创建时间：2023/8/27 20:05:34
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.Domain.Repositories;
using Sgr.UPMS.Domain.Departments;
using Sgr.UPMS.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Departments
{
    public class DeleteDepartmentCommandHandle : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DeleteDepartmentCommandHandle(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _departmentRepository.GetAsync(request.DepartmentId, cancellationToken);
            if (entity == null)
                return false;

            var sons = await _departmentRepository.GetChildNodesRecursionAsync(entity!, cancellationToken);

            entity.AddDomainEvent(new DepartmentDeleteDomainEvent(entity.Id));
            await _departmentRepository.DeleteAsync(entity!, cancellationToken);

            foreach(var son in sons)
            {
                son.AddDomainEvent(new DepartmentDeleteDomainEvent(son.Id));
                await _departmentRepository.DeleteAsync(son!, cancellationToken);
            }

            return await _departmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
