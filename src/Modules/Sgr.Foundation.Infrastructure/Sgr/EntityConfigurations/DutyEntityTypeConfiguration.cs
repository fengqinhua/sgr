using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Sgr.DutyAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sgr.EntityConfigurations
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
