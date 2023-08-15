using Sgr.Domain.Entities.Auditing;
using Sgr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.DataDictionaryAggregate
{
    public class DataCategoryItem : CreationAndModifyAuditedEntity<long, long>, IAggregateRoot, IOptimisticLock
    {
        private DataCategoryItem() { }

        /// <summary>
        /// 字典分类标识
        /// </summary>
        public long CategoryTypeId { get; protected set; }
        /// <summary>
        /// 字典项编码
        /// </summary>
        public string DcItemCode { get; protected set; } = string.Empty;
        /// <summary>
        /// 字典项值
        /// </summary>
        public string DcItemValue { get; protected set; } = string.Empty;
        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool IsEditable { get; protected set; } = true;
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;
        /// <summary>
        /// 状态
        /// </summary>
        public EntityStates State { get; internal protected set; } = EntityStates.Normal;



        #region IOptimisticLock (乐观锁)

        /// <summary>
        /// 行版本
        /// </summary>
        public long RowVersion { get; set; }

        #endregion

    }
}
