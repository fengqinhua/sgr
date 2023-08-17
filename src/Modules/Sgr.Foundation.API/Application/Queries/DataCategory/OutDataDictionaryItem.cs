﻿/**************************************************************
 * 
 * 唯一标识：4c38a282-7881-488c-91aa-f191c1d86728
 * 命名空间：Sgr.Foundation.API.Application.Queries.DataDictionary
 * 创建时间：2023/8/17 15:59:25
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;

namespace Sgr.Foundation.API.Application.Queries.DataCategory
{
    public class OutDataDictionaryItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 上级Id
        /// </summary>
        public long ParentId { get; set; } = 0;
        /// <summary>
        /// 字典分类标识
        /// </summary>
        public string CategoryTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// 字典项名称
        /// </summary>
        public string DcItemName { get; set; } = string.Empty;
        /// <summary>
        /// 字典项值
        /// </summary>
        public string DcItemValue { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool IsEditable { get; set; } = true;
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;
        /// <summary>
        /// 状态
        /// </summary>
        public EntityStates State { get; set; } = EntityStates.Normal;
    }
}
