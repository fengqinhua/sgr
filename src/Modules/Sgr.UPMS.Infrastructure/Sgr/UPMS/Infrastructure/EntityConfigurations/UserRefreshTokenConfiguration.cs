/**************************************************************
 * 
 * 唯一标识：9159efe9-3280-4078-8037-5cc8003811a8
 * 命名空间：Sgr.UPMS.Infrastructure.EntityConfigurations
 * 创建时间：2023/8/28 9:28:42
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Domain.UserTokens;

namespace Sgr.UPMS.Infrastructure.EntityConfigurations
{
    internal class UserRefreshTokenConfiguration : EntityTypeConfigurationBase<UserRefreshToken, long>
    {
        public override void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("sgr_user_refreshtoken");

            base.Configure(builder);

            builder.PropertyAndHasColumnName(b => b.Token, GetColumnNameCase())
                .HasMaxLength(100)
                .IsRequired()
                .HasComment("令牌内容");

            builder.PropertyAndHasColumnName(b => b.Expires, GetColumnNameCase())
                .IsRequired()
                .HasComment("失效时间");

            builder.PropertyAndHasColumnName(b => b.UserId, GetColumnNameCase())
                .IsRequired()
                .HasComment("用户标识");

            builder.HasIndex("UserId");
            builder.HasIndex("Token");
        }
    }
}
