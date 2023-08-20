/**************************************************************
 * 
 * 唯一标识：fb3ca4e4-6975-4739-aacb-5b4cd58f99f5
 * 命名空间：Sgr.Foundation.API.Application.DataCategory.Commands
 * 创建时间：2023/8/17 21:16:48
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
    public class DeleteCategpryItemCommandHandle : IRequestHandler<DeleteCategpryItemCommand, bool>
    {
        private readonly IDataCategoryItemManage _dataCategoryItemManage;

        public DeleteCategpryItemCommandHandle(IDataCategoryItemManage dataCategoryItemManage)
        {
            _dataCategoryItemManage = dataCategoryItemManage;
        }

        public async Task<bool> Handle(DeleteCategpryItemCommand request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            return await _dataCategoryItemManage.RemoveNodeAsync(request.Id, cancellationToken);
        }
    }
}
