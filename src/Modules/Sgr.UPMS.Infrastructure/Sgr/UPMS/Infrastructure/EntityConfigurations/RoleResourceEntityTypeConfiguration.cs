using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Sgr.UPMS.Domain.Roles;

namespace Sgr.UPMS.Infrastructure.EntityConfigurations
{
    internal class RoleResourceEntityTypeConfiguration : EntityTypeConfigurationBase<RoleResource, string>
    {
        public override void Configure(EntityTypeBuilder<RoleResource> builder)
        {
            builder.ToTable("sgr_role_resource");

            base.Configure(builder);

            builder.Property(f => f.Id).HasValueGenerator<StringValueGenerator>();

            builder.PropertyAndHasColumnName(b => b.ResourceCode, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("资源标识");

            builder.PropertyAndHasColumnName(b => b.ResourceType, GetColumnNameCase())
                .IsRequired()
                .HasComment("资源类型");

            builder.PropertyAndHasColumnName<long>("RoleId")
                .IsRequired()
                .HasComment("角色标识");

            //设置索引
            builder.HasIndex("RoleId");
        }


    }
}
