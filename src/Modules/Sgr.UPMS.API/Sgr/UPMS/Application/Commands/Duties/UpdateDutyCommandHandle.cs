/**************************************************************
 * 
 * 唯一标识：02ecd139-a283-4f2a-b477-1bab845c6121
 * 命名空间：Sgr.UPMS.Application.Commands.Duties
 * 创建时间：2023/8/27 19:21:24
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.UPMS.Domain.Duties;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Application.Commands.Duties
{
    public class UpdateDutyCommandHandle : IRequestHandler<UpdateDutyCommand, bool>
    {
        private readonly IDutyRepository _dutyRepository;

        public UpdateDutyCommandHandle(IDutyRepository dutyRepository)
        {
            _dutyRepository = dutyRepository;
        }

        public async Task<bool> Handle(UpdateDutyCommand request, CancellationToken cancellationToken)
        {
            var duty = await _dutyRepository.GetAsync(request.DutyId, cancellationToken);

            if (duty == null)
                return false;

            duty.Name = request.Name;
            duty.Remarks = request.Remarks;
            duty.OrderNumber = request.OrderNumber;

            duty.ChangeEntityStates(request.State);

            await _dutyRepository.UpdateAsync(duty, cancellationToken);

            return await _dutyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
