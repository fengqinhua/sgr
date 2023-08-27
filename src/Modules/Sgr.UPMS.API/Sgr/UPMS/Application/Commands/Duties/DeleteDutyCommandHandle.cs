/**************************************************************
 * 
 * 唯一标识：47d4ea4f-9257-467f-913a-1db9f859f85f
 * 命名空间：Sgr.UPMS.Application.Commands.Duties
 * 创建时间：2023/8/27 19:24:00
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
    public class DeleteDutyCommandHandle : IRequestHandler<DeleteDutyCommand, bool>
    {
        private readonly IDutyRepository _dutyRepository;

        public DeleteDutyCommandHandle(IDutyRepository dutyRepository)
        {
            _dutyRepository = dutyRepository;
        }

        public async Task<bool> Handle(DeleteDutyCommand request, CancellationToken cancellationToken)
        {
            await _dutyRepository.DeleteAsync(request.DutyId, cancellationToken);

            return await _dutyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
