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

    internal class UserEntityTypeConfiguration : EntityTypeConfigurationBase<User, long>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("sgr_user");

            base.Configure(builder);

            builder.PropertyAndHasColumnName(b => b.LoginName, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("登录名称");

            builder.PropertyAndHasColumnName(b => b.LoginPassword, GetColumnNameCase())
                .HasMaxLength(100)
                .IsRequired()
                .HasComment("登录密码");

            builder.PropertyAndHasColumnName(b => b.FirstLoginTime, GetColumnNameCase())
                .HasComment("首次登录时间");

            builder.PropertyAndHasColumnName(b => b.LastLoginTime, GetColumnNameCase())
                .HasComment("最近一次登录时间");

            builder.PropertyAndHasColumnName(b => b.LoginSuccessCount, GetColumnNameCase())
                .IsRequired()
                .HasComment("登录成功次数");

            builder.PropertyAndHasColumnName(b => b.LoginFailCount, GetColumnNameCase())
                .IsRequired()
                .HasComment("登录失败次数");

            builder.PropertyAndHasColumnName(b => b.UserName, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("用户姓名");

            builder.PropertyAndHasColumnName(b => b.UserPhone, GetColumnNameCase())
                .HasMaxLength(30)
                .HasComment("用户绑定的手机号码");

            builder.PropertyAndHasColumnName(b => b.QQ, GetColumnNameCase())
                .HasMaxLength(20)
                .HasComment("用户QQ号码");

            builder.PropertyAndHasColumnName(b => b.UserEmail, GetColumnNameCase())
                .HasMaxLength(100)
                .HasComment("用户邮箱地址");

            builder.PropertyAndHasColumnName(b => b.Wechat, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("用户微信号码");

            builder.PropertyAndHasColumnName(b => b.IsSuperAdmin, GetColumnNameCase())
                .IsRequired();

            builder.PropertyAndHasColumnName(b => b.State, GetColumnNameCase())
                .IsRequired()
                .HasComment("状态");

            //部门
            builder.PropertyAndHasColumnName(b => b.DepartmentId, GetColumnNameCase())
                .HasComment("所属部门Id");

            builder.PropertyAndHasColumnName(b => b.DepartmentName, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("所属部门名称");

            //职务
            builder.HasMany(b => b.Duties)
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata
                .FindNavigation(nameof(User.Duties))
                ?.SetPropertyAccessMode(PropertyAccessMode.Field);

            //角色
            builder.HasMany(b => b.Roles)
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata
                .FindNavigation(nameof(User.Roles))
                ?.SetPropertyAccessMode(PropertyAccessMode.Field);


            //设置索引
            builder.HasIndex(f => f.LoginName);
            builder.HasIndex(f => f.DepartmentId);

        }
    }
}
