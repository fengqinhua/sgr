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
using Sgr.OrganizationAggregate;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Sgr.EntityFrameworkCore;

namespace Sgr.EntityConfigurations
{
    internal class OrganizationEntityTypeConfiguration : EntityTypeConfigurationBase<Organization,long>
    {
        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            //builder.ToTable("organization", SgrDbContext.DEFAULT_SCHEMA);

            builder.ToTable("organization");

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
    }
}