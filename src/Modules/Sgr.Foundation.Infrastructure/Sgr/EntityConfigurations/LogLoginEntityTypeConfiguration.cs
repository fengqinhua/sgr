using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.AuditLogAggregate;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Sgr.OrganizationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityConfigurations
{
    internal class LogLoginEntityTypeConfiguration : EntityTypeConfigurationBase<LogLogin, long>
    {
        public override void Configure(EntityTypeBuilder<LogLogin> builder)
        {
            builder.ToTable("sgr_loglogin");

            base.Configure(builder);

            builder.PropertyAndHasColumnName(b => b.LoginName, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("登录账号");

            builder.PropertyAndHasColumnName(b => b.UserName, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("用户姓名");

            builder.PropertyAndHasColumnName(b => b.LoginTime, GetColumnNameCase())
                .IsRequired()
                .HasComment("登录时间");

            builder.PropertyAndHasColumnName(b => b.IpAddress, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("登录Ip地址");

            builder.PropertyAndHasColumnName(b => b.Location, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("登录地点");

            builder.PropertyAndHasColumnName(b => b.LoginWay, GetColumnNameCase())
                .HasMaxLength(20)
                .HasComment("登录途径");

            builder.PropertyAndHasColumnName(b => b.LoginProvider, GetColumnNameCase())
                .HasMaxLength(500);

            builder.PropertyAndHasColumnName(b => b.ProviderKey, GetColumnNameCase())
                .HasMaxLength(500);

            builder.PropertyAndHasColumnName(b => b.ProviderDisplayName, GetColumnNameCase())
                .HasMaxLength(500);

            builder.PropertyAndHasColumnName(b => b.ClientBrowser, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("客户端浏览器");

            builder.PropertyAndHasColumnName(b => b.ClientOs, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("客户端系统");

            builder.PropertyAndHasColumnName(b => b.Status, GetColumnNameCase())
                .IsRequired()
                .HasComment("登录状态");

            builder.PropertyAndHasColumnName(b => b.Remark, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("登录描述");
        }
    }
}
