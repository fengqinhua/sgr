/**************************************************************
 * 
 * 唯一标识：5245e864-54e3-4044-af0c-f0c9799c1865
 * 命名空间：Sgr.Foundation.API.Application.Queries.DataDictionary
 * 创建时间：2023/8/17 15:30:28
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Application.ViewModels;
using Sgr.Foundation.API.Application.DataCategory.ViewModels;
using System.Threading.Tasks;

namespace Sgr.Foundation.API.Application.DataCategory.Queries
{
    public interface IDataCategoryQueries
    {
        /// <summary>
        /// 获取所有数据字典分类
        /// </summary>
        /// <returns></returns>
        Task<NameValue[]> GetCategoryTypesAsync();
        /// <summary>
        /// 获取指定数据指点分类相关的字典项集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResponse<DataDictionaryItemModel>> GetItemsByCategoryTypeAsync(CategpryItemSearchModel request);
        /// <summary>
        /// 获取字典项明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataDictionaryItemModel?> GetItemByIdAsync(long id);
    }
}
