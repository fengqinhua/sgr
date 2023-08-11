using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Sgr.RoleAggregate;
using Sgr.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityConfigurations
{
    internal class UserDutyEntityTypeConfiguration : EntityTypeConfigurationBase<UserDuty, long>
    {
        public override void Configure(EntityTypeBuilder<UserDuty> builder)
        {
            builder.ToTable("sgr_user_duty");

            base.Configure(builder);

            builder.PropertyAndHasColumnName(b => b.DutyId, GetColumnNameCase())
                .IsRequired()
                .HasComment("岗位标识");

            builder.PropertyAndHasColumnName<long>("UserId")
                .IsRequired()
                .HasComment("用户标识");
        }
    }
}
