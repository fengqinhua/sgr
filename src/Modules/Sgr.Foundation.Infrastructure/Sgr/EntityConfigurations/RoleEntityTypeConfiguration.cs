using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.RoleAggregate;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sgr.UserAggregate;

namespace Sgr.EntityConfigurations
{
    internal class RoleEntityTypeConfiguration : EntityTypeConfigurationBase<Role, long>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("sgr_role");

            base.Configure(builder);

            builder.PropertyAndHasColumnName(b => b.Code, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("角色编码");

            builder.PropertyAndHasColumnName(b => b.RoleName, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("角色名称");

            builder.PropertyAndHasColumnName(b => b.OrderNumber, GetColumnNameCase())
                .IsRequired()
                .HasComment("排序号");

            builder.PropertyAndHasColumnName(b => b.State, GetColumnNameCase())
                .IsRequired()
                .HasComment("状态");

            builder.PropertyAndHasColumnName(b => b.Remarks, GetColumnNameCase())
                .HasMaxLength(500)
                .HasComment("备注");

            //资源
            builder.HasMany(b => b.Resources)
                .WithOne()
                .HasForeignKey("RoleId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata
                .FindNavigation(nameof(Role.Resources))
                ?.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
