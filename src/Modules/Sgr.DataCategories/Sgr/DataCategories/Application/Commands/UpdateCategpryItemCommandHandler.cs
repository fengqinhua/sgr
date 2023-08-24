/**************************************************************
 * 
 * 唯一标识：f5096d64-be17-45ac-bc78-03045aa091d2
 * 命名空间：Sgr.DataCategories.Application.Commands
 * 创建时间：2023/8/17 18:52:25
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.DataCategories.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.DataCategories.Application.Commands
{
    public class UpdateCategpryItemCommandHandler : IRequestHandler<UpdateCategpryItemCommand, bool>
    {
        private readonly IDataCategoryItemManage _dataCategoryItemManage;
        private readonly IDataCategoryItemRepository _dataCategoryItemRepository;

        public UpdateCategpryItemCommandHandler(IDataCategoryItemManage dataCategoryItemManage, IDataCategoryItemRepository dataCategoryItemRepository)
        {
            _dataCategoryItemManage = dataCategoryItemManage;
            _dataCategoryItemRepository = dataCategoryItemRepository;
        }

        public async Task<bool> Handle(UpdateCategpryItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dataCategoryItemRepository.GetAsync(request.Id, cancellationToken);

            if (entity == null || !entity.IsEditable)
                return false;

            entity.DcItemName = request.DcItemName;
            entity.Remarks = request.Remarks;
            entity.OrderNumber = request.OrderNumber;
            entity.State = request.State;

            await _dataCategoryItemRepository.UpdateAsync(entity, cancellationToken);

            if (entity.ParentId != request.ParentId)
            {
                await _dataCategoryItemManage.ChangeParentIdAsync(entity, request.ParentId, 5, cancellationToken);
            }

            return await _dataCategoryItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
