/**************************************************************
 * 
 * 唯一标识：9a35a5fe-eec9-4aa2-9435-21e997ecf21a
 * 命名空间：Sgr.UPMS.Application.Commands.Duties
 * 创建时间：2023/8/27 19:16:09
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Application.Commands.Roles;
using Sgr.UPMS.Domain.Duties;
using Sgr.UPMS.Domain.Roles;
using Sgr.UPMS.Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Duties
{
    public class CreateDutyCommandHandle : IRequestHandler<CreateDutyCommand, bool>
    {
        private readonly IDutyRepository _dutyRepository;

        public CreateDutyCommandHandle(IDutyRepository dutyRepository)
        {
            _dutyRepository = dutyRepository;
        }

        public async Task<bool> Handle(CreateDutyCommand request, CancellationToken cancellationToken)
        {
            Duty role = new Duty(Guid.NewGuid().ToString("N"),
                request.Name,
                request.OrderNumber,
                request.Remarks,
                request.OrgId);

            await _dutyRepository.InsertAsync(role, cancellationToken);

            return await _dutyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
