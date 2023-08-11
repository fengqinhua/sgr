using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.AuditLogAggregate;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityConfigurations
{
 
    internal class LogOperateEntityTypeConfiguration : EntityTypeConfigurationBase<LogOperate, long>
    {
        public override void Configure(EntityTypeBuilder<LogOperate> builder)
        {
            builder.ToTable("sgr_logoperate");

            base.Configure(builder);

            builder.PropertyAndHasColumnName(b => b.LoginName, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("登录账号");

            builder.PropertyAndHasColumnName(b => b.UserName, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("用户姓名");

            builder.PropertyAndHasColumnName(b => b.IpAddress, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("登录Ip地址");

            builder.PropertyAndHasColumnName(b => b.Location, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("登录地点");

            builder.PropertyAndHasColumnName(b => b.LoginWay, GetColumnNameCase())
                .HasMaxLength(20)
                .HasComment("登录途径");

            builder.PropertyAndHasColumnName(b => b.ClientBrowser, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("客户端浏览器");

            builder.PropertyAndHasColumnName(b => b.ClientOs, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("客户端系统");

            builder.PropertyAndHasColumnName(b => b.RequestDescription, GetColumnNameCase())
                .IsRequired()
                .HasMaxLength(200)
                .HasComment("请求说明");

            builder.PropertyAndHasColumnName(b => b.RequestUrl, GetColumnNameCase())
                .IsRequired()
                .HasMaxLength(500)
                .HasComment("请求地址");

            builder.PropertyAndHasColumnName(b => b.HttpMethod, GetColumnNameCase())
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("请求方法");

            builder.PropertyAndHasColumnName(b => b.RequestParam, GetColumnNameCase())
                .IsRequired()
                .HasMaxLength(2000)
                .HasComment("请求参数");

            builder.PropertyAndHasColumnName(b => b.RequestTime, GetColumnNameCase())
                .IsRequired()
                .HasComment("请求时间");

            builder.PropertyAndHasColumnName(b => b.RequestDuration, GetColumnNameCase())
                .HasComment("请求耗时");

            builder.PropertyAndHasColumnName(b => b.Status, GetColumnNameCase())
                .IsRequired()
                .HasComment("请求结果");

            builder.PropertyAndHasColumnName(b => b.Remark, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("请求结果描述");
        }
    }
}

