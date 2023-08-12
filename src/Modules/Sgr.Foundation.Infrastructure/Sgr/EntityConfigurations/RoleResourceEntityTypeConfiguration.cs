using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Sgr.DepartmentAggregate;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Sgr.RoleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityConfigurations
{
    internal class RoleResourceEntityTypeConfiguration : EntityTypeConfigurationBase<RoleResource, string>
    {
        public override void Configure(EntityTypeBuilder<RoleResource> builder)
        {
            builder.ToTable("sgr_role_resource");

            builder.Property(f => f.Id).HasValueGenerator<StringValueGenerator2>();

            base.Configure(builder);

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

    public class StringValueGenerator2 : ValueGenerator<string>
    {
        //
        // 摘要:
        //     Gets a value indicating whether the values generated are temporary or permanent.
        //     This implementation always returns false, meaning the generated values will be
        //     saved to the database.
        public override bool GeneratesTemporaryValues
        {
            get
            {
                return false;
            }
        }

        //
        // 摘要:
        //     Gets a value to be assigned to a property.
        //
        // 返回结果:
        //     The value to be assigned to a property.
        public override string Next(EntityEntry entry)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
