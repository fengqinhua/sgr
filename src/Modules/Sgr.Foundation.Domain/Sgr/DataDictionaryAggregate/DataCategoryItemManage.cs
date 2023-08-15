using Sgr.Domain.Entities;
using Sgr.Domain.Managers;
using Sgr.Domain.Repositories;
using Sgr.Exceptions;
using Sgr.Generator;
using Sgr.OrganizationAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.DataDictionaryAggregate
{
    public class DataCategoryItemManage : TreeNodeManageBase<DataCategoryItem, long>, IDataCategoryItemManage
    {
        private readonly IDataCategoryItemChecker _dataCategoryItemChecker;
        private readonly INumberIdGenerator _numberIdGenerator;

        public DataCategoryItemManage(IDataCategoryItemRepository repository,
            IDataCategoryItemChecker dataCategoryItemChecker,
            INumberIdGenerator numberIdGenerator)
            : base(repository)
        {
            _dataCategoryItemChecker = dataCategoryItemChecker;
            _numberIdGenerator = numberIdGenerator;
        }

        public virtual async Task<DataCategoryItem> CreateNewAsync(
            string name,
            string value,
            string categoryTypeCode,
            int orderNumber,
            long parentId = 0)
        {
            //不可为空
            Check.StringNotNullOrWhiteSpace(name, nameof(name));
            Check.StringNotNullOrWhiteSpace(value, nameof(value));
            Check.StringNotNullOrWhiteSpace(categoryTypeCode, nameof(categoryTypeCode));

            //检查组织机构编码是否符合规范
            if (await _dataCategoryItemChecker.ValueIsUniqueAsync(value, categoryTypeCode))
                throw new BusinessException($"字典项值{value}已存在");

            //获取Id及Id-Path
            if(!long.TryParse(value, out long id))
                id = _numberIdGenerator.GenerateUniqueId();

            string nodePath =  (await base.GetNodePath(parentId, id, 5));
            if(nodePath == "0")
                nodePath = categoryTypeCode;

            //返回
            return new DataCategoryItem()
            {
                Id = id,
                CategoryTypeCode = categoryTypeCode,
                DcItemName = name,
                DcItemValue = value,
                IsEditable = true,
                OrderNumber = orderNumber,
                ParentId = parentId,
                NodePath = nodePath,
                State = EntityStates.Normal
            };
        }


        protected override void ChangeTreeNodePath(DataCategoryItem entity, string nodePath)
        {
            entity.NodePath = nodePath;
        }
    }
}
