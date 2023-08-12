﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Sgr.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityConfigurations
{
    internal class UserRoleEntityTypeConfiguration : EntityTypeConfigurationBase<UserRole, string>
    {
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("sgr_user_role");

            base.Configure(builder);

            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.PropertyAndHasColumnName(b => b.RoleId, GetColumnNameCase())
                .IsRequired()
                .HasComment("角色标识");

            builder.PropertyAndHasColumnName<long>("UserId")
                .IsRequired()
                .HasComment("用户标识");

            //设置索引
            builder.HasIndex("UserId");
        }
    }
}