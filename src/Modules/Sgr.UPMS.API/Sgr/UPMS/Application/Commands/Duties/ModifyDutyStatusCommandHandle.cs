/**************************************************************
 * 
 * 唯一标识：d87572d1-8a25-4761-aa67-290c9f79619a
 * 命名空间：Sgr.UPMS.Application.Commands.Duties
 * 创建时间：2023/8/27 19:25:17
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
    public class ModifyDutyStatusCommandHandle : IRequestHandler<ModifyDutyStatusCommand, bool>
    {
        private readonly IDutyRepository _dutyRepository;

        public ModifyDutyStatusCommandHandle(IDutyRepository dutyRepository)
        {
            _dutyRepository = dutyRepository;
        }

        public async Task<bool> Handle(ModifyDutyStatusCommand request, CancellationToken cancellationToken)
        {
            var duty = await _dutyRepository.GetAsync(request.DutyId, cancellationToken);

            if (duty == null)
                return false;

            duty.ChangeEntityStates(request.State);

            await _dutyRepository.UpdateAsync(duty, cancellationToken);

            return await _dutyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
