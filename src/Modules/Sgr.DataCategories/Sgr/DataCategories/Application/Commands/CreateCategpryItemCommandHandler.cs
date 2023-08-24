/**************************************************************
 * 
 * 唯一标识：6c0d0341-3e10-4b9e-aac8-c9361751ae55
 * 命名空间：Sgr.DataCategories.Application.Commands
 * 创建时间：2023/8/17 18:45:18
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
    public class CreateCategpryItemCommandHandler : IRequestHandler<CreateCategpryItemCommand, bool>
    {
        private readonly IDataCategoryItemManage _dataCategoryItemManage;
        private readonly IDataCategoryItemRepository _dataCategoryItemRepository;

        public CreateCategpryItemCommandHandler(IDataCategoryItemManage dataCategoryItemManage, IDataCategoryItemRepository dataCategoryItemRepository)
        {
            _dataCategoryItemManage = dataCategoryItemManage;
            _dataCategoryItemRepository = dataCategoryItemRepository;
        }

        public async Task<bool> Handle(CreateCategpryItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dataCategoryItemManage.CreateNewAsync(request.DcItemName,
                   request.DcItemValue,
                   request.Remarks,
                   request.CategoryTypeCode,
                   request.OrderNumber,
                   request.ParentId);

            await _dataCategoryItemRepository.InsertAsync(entity, cancellationToken);

            return await _dataCategoryItemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
