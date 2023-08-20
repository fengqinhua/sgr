using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Sgr.UPMS.Domain.Departments;

namespace Sgr.UPMS.Infrastructure.EntityConfigurations
{
    internal class DepartmentEntityTypeConfiguration : EntityTypeConfigurationBase<Department, long>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("sgr_department");

            base.Configure(builder);

            builder.PropertyAndHasColumnName(b => b.Code, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("部门编码");

            builder.PropertyAndHasColumnName(b => b.Name, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("部门名称");

            builder.PropertyAndHasColumnName(b => b.OrderNumber, GetColumnNameCase())
                .IsRequired()
                .HasComment("组织排序号");

            builder.PropertyAndHasColumnName(b => b.Leader, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("负责人");

            builder.PropertyAndHasColumnName(b => b.Phone, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("联系电话");

            builder.PropertyAndHasColumnName(b => b.Email, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("联系邮箱");

            builder.PropertyAndHasColumnName(b => b.State, GetColumnNameCase())
                .IsRequired()
                .HasComment("部门状态");

            builder.PropertyAndHasColumnName(b => b.Remarks, GetColumnNameCase())
                .HasMaxLength(500)
                .HasComment("描述");
        }
    }
}
