/**************************************************************
 * 
 * 唯一标识：36df43e0-bd68-4736-8661-9ad221a8c08e
 * 命名空间：Sgr.EntityConfigurations
 * 创建时间：2023/8/4 9:13:06
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sgr.OrganizationAggregate;
using Sgr.Domain.Entities;
using Sgr.EntityFrameworkCore.EntityConfigurations;

namespace Sgr.EntityConfigurations
{
    internal class OrganizationEntityTypeConfiguration : EntityTypeConfigurationBase<Organization,long>
    {
        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("organization", FoundationContext.DEFAULT_SCHEMA);

            base.Configure(builder);

            builder.Property(b => b.Code)
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("组织机构编码");

            builder.Property(b => b.Name)
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("组织机构名称");

            builder.Property(b => b.OrgTypeCode)
                .HasMaxLength(50)
                .IsRequired()
                .HasComment("组织机构类型编码");

            builder.Property(b => b.AreaCode)
                .HasMaxLength(50)
                .IsRequired()
                .HasComment("所属行政区划编码");

            builder.Property(b => b.OrderNumber)
                .IsRequired()
                .HasComment("组织机构排序号");

            builder.Property(b => b.Leader)
                .HasMaxLength(50)
                .HasComment("机构负责人");

            builder.Property(b => b.Phone)
                .HasMaxLength(50)
                .HasComment("联系电话");

            builder.Property(b => b.Email)
                .HasMaxLength(50)
                .HasComment("联系邮箱");

            builder.Property(b => b.Address)
                .HasMaxLength(200)
                .HasComment("所在地址");

            builder.Property(b => b.LogoUrl)
                .HasMaxLength(200)
                .HasComment("Logo地址");

            builder.Property(b => b.State)
                .IsRequired()
                .HasComment("组织机构状态");

        }

        //public override void Configure(EntityTypeBuilder<Organization> orgConfiguration)
        //{


        //    orgConfiguration.ToTable("organization", FoundationContext.DEFAULT_SCHEMA,
        //        t => t.HasComment("组织机构"));

        //    orgConfiguration.Ignore(b => b.DomainEvents);

        //    orgConfiguration.HasKey(b => b.Id);


        //    orgConfiguration.Property(b => b.Address)
        //        .HasMaxLength(200)
        //        .IsRequired()
        //        .HasComment("地址");

        //    //buyerConfiguration.HasIndex("IdentityGuid")
        //    //    .IsUnique(true);

        //    //buyerConfiguration.Property(b => b.Name);

        //    //buyerConfiguration.HasMany(b => b.PaymentMethods)
        //    //    .WithOne()
        //    //    .HasForeignKey("BuyerId")
        //    //    .OnDelete(DeleteBehavior.Cascade);

        //    //var navigation = buyerConfiguration.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));

        //    //navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        //}


    }
}