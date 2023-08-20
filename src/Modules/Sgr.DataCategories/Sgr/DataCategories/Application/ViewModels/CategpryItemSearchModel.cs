/**************************************************************
 * 
 * 唯一标识：f27bc731-00d1-4c07-864a-9ecd4835f63b
 * 命名空间：Sgr.DataCategories.Application.ViewModels
 * 创建时间：2023/8/17 16:09:24
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Application.ViewModels;
using Sgr.Domain.Entities;

namespace Sgr.DataCategories.Application.ViewModels
{
    public class CategpryItemSearchModel : PagedRequest
    {
        /// <summary>
        /// 字典分类标识
        /// </summary>
        public string CategoryTypeCode { get; set; } = "";
        /// <summary>
        /// 字典项名称
        /// </summary>
        public string? DcItemName { get; set; }
        /// <summary>
        /// 操作状态
        /// </summary>
        public EntityStates? States { get; set; }
    }
}
