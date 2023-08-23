/**************************************************************
 * 
 * 唯一标识：122da195-86ac-482e-b202-ce2ee92cdfe0
 * 命名空间：Sgr.DataCategories.Application.Queries
 * 创建时间：2023/8/17 16:23:34
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Sgr.Application.ViewModels;
using Sgr.DataCategories.Application.ViewModels;
using Sgr.DataCategories.Domain;
using Sgr.DataCategories.Services;
using Sgr.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Sgr.DataCategories.Application.Queries
{
    public class DataCategoryQueries : IDataCategoryQueries
    {
        private readonly SgrDbContext _context;
        private readonly ICategoryTypeService _categoryTypeService;

        public DataCategoryQueries(SgrDbContext context, ICategoryTypeService categoryTypeService)
        {
            _context = context;
            _categoryTypeService = categoryTypeService;
        }

        public Task<NameValue[]> GetCategoryTypesAsync()
        {
            return _categoryTypeService.GetAllAsync();
        }

        public Task<DataDictionaryItemModel?> GetItemByIdAsync(long id)
        {
            return _context.Set<DataCategoryItem>()
                 .Where(f => f.Id == id)
                 .Select(f => new DataDictionaryItemModel()
                 {
                     CategoryTypeCode = f.CategoryTypeCode,
                     DcItemName = f.DcItemName,
                     DcItemValue = f.DcItemValue,
                     Id = f.Id,
                     IsEditable = f.IsEditable,
                     OrderNumber = f.OrderNumber,
                     ParentId = f.ParentId,
                     Remarks = f.Remarks,
                     State = f.State
                 })
                 .FirstOrDefaultAsync();
        }

        public async Task<PagedResponse<DataDictionaryItemModel>> GetItemsByCategoryTypeAsync(CategpryItemSearchModel request)
        {
            Check.NotNull(request, nameof(request));

            //设置查询条件
            IQueryable<DataCategoryItem> query = _context.Set<DataCategoryItem>()
                .Where(f => f.CategoryTypeCode == request.CategoryTypeCode);

            if (!string.IsNullOrEmpty(request.DcItemName))
                query = query.Where(f => EF.Functions.Like(f.DcItemName, $"%{request.DcItemName}%"));

            if (request.States != null)
                query = query.Where(f => f.State == request.States!);

            query = request.IsAscending ? query.OrderBy(f => f.OrderNumber) : query.OrderByDescending(f => f.OrderNumber);

            return await query.Select(f => new DataDictionaryItemModel()
            {
                CategoryTypeCode = f.CategoryTypeCode,
                DcItemName = f.DcItemName,
                DcItemValue = f.DcItemValue,
                Id = f.Id,
                IsEditable = f.IsEditable,
                OrderNumber = f.OrderNumber,
                ParentId = f.ParentId,
                Remarks = f.Remarks,
                State = f.State
            }).ToPagedListByPageSizeAsync(request.PageIndex, request.PageSize);
        }
    }
}
