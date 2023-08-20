using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.DataCategories.Domain;
using Sgr.EntityFrameworkCore.EntityConfigurations;

namespace Sgr.DataCategories.Infrastructure.EntityConfigurations
{
    internal class DataCategoryItemConfiguration : EntityTypeConfigurationBase<DataCategoryItem, long>
    {
        public override void Configure(EntityTypeBuilder<DataCategoryItem> builder)
        {
            builder.ToTable("sgr_datacategoryitem");

            base.Configure(builder);

            builder.PropertyAndHasColumnName(b => b.CategoryTypeCode, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("字典分类标识");

            builder.PropertyAndHasColumnName(b => b.DcItemName, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("字典项名称");

            builder.PropertyAndHasColumnName(b => b.DcItemValue, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("字典项值");

            builder.PropertyAndHasColumnName(b => b.IsEditable, GetColumnNameCase())
                .IsRequired()
                .HasComment("是否可编辑");

            builder.PropertyAndHasColumnName(b => b.OrderNumber, GetColumnNameCase())
                .IsRequired()
                .HasComment("排序号");

            builder.PropertyAndHasColumnName(b => b.State, GetColumnNameCase())
                .IsRequired()
                .HasComment("组织机构状态");

            builder.PropertyAndHasColumnName(b => b.Remarks, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("备注");

            //设置索引
            builder.HasIndex(f => f.CategoryTypeCode);

            //初始数据
            //builder.HasData()
        }
    }
}
