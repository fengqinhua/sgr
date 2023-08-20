using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Sgr.UPMS.Domain.Roles;

namespace Sgr.UPMS.Infrastructure.EntityConfigurations
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
