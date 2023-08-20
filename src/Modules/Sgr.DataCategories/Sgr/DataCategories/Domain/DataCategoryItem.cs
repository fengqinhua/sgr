using Sgr.Domain.Entities.Auditing;
using Sgr.Domain.Entities;

namespace Sgr.DataCategories.Domain
{
    public class DataCategoryItem : CreationAndModifyAuditedEntity<long, long>, IAggregateRoot, IOptimisticLock, ITreeNode<long>
    {
        internal DataCategoryItem() { }

        //internal DataCategoryItem(string categoryTypeCode,string dcItemValue, bool isEditable = true)
        //{
        //    CategoryTypeCode = categoryTypeCode;
        //    DcItemValue = dcItemValue;
        //    IsEditable = isEditable;
        //    State = EntityStates.Normal;
        //}

        /// <summary>
        /// 字典分类标识
        /// </summary>
        public string CategoryTypeCode { get; internal protected set; } = string.Empty;
        /// <summary>
        /// 字典项名称
        /// </summary>
        public string DcItemName { get; set; } = string.Empty;
        /// <summary>
        /// 字典项值
        /// </summary>
        public string DcItemValue { get; internal protected set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool IsEditable { get; internal protected set; } = true;
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;
        /// <summary>
        /// 状态
        /// </summary>
        public EntityStates State { get; set; } = EntityStates.Normal;


        #region  ITreeNode (树形结构)

        /// <summary>
        /// 上级节点Id
        /// </summary>
        public long ParentId { get; internal protected set; } = 0;
        /// <summary>
        /// 树节点层次目录
        /// </summary>
        public string NodePath { get; internal protected set; } = string.Empty;

        #endregion

        #region IOptimisticLock (乐观锁)

        /// <summary>
        /// 行版本
        /// </summary>
        public long RowVersion { get; set; }

        #endregion

    }
}
