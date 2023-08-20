using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Sgr.UPMS.Domain.Duties;

namespace Sgr.UPMS.Infrastructure.EntityConfigurations
{
    internal class DutyEntityTypeConfiguration : EntityTypeConfigurationBase<Duty, long>
    {
        public override void Configure(EntityTypeBuilder<Duty> builder)
        {
            builder.ToTable("sgr_duty");

            base.Configure(builder);

            builder.PropertyAndHasColumnName(b => b.Code, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("职务编码");

            builder.PropertyAndHasColumnName(b => b.Name, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("职务名称");

            builder.PropertyAndHasColumnName(b => b.OrderNumber, GetColumnNameCase())
                .IsRequired()
                .HasComment("排序号");

            builder.PropertyAndHasColumnName(b => b.State, GetColumnNameCase())
                .IsRequired()
                .HasComment("职务状态");

            builder.PropertyAndHasColumnName(b => b.Remarks, GetColumnNameCase())
                .HasMaxLength(500)
                .HasComment("职务备注");


        }
    }
}
