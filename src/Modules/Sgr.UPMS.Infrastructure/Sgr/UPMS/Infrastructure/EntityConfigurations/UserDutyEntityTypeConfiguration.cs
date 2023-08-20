using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Sgr.UPMS.Domain.Users;

namespace Sgr.UPMS.Infrastructure.EntityConfigurations
{
    internal class UserDutyEntityTypeConfiguration : EntityTypeConfigurationBase<UserDuty, string>
    {
        public override void Configure(EntityTypeBuilder<UserDuty> builder)
        {
            builder.ToTable("sgr_user_duty");

            base.Configure(builder);

            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.PropertyAndHasColumnName(b => b.DutyId, GetColumnNameCase())
                .IsRequired()
                .HasComment("岗位标识");

            builder.PropertyAndHasColumnName<long>("UserId")
                .IsRequired()
                .HasComment("用户标识");

            //设置索引
            builder.HasIndex("UserId");
        }
    }
}
