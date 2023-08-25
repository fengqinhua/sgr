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
using Sgr.EntityFrameworkCore.EntityConfigurations;
using Sgr.UPMS.Domain.Organizations;

namespace Sgr.UPMS.Infrastructure.EntityConfigurations
{
    internal class OrganizationEntityTypeConfiguration : EntityTypeConfigurationBase<Organization, long>
    {
        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("sgr_organization");

            base.Configure(builder);

            builder.PropertyAndHasColumnName(b => b.Code, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("组织机构编码");

            builder.PropertyAndHasColumnName(b => b.Name, GetColumnNameCase())
                .HasMaxLength(200)
                .IsRequired()
                .HasComment("组织机构名称");

            builder.PropertyAndHasColumnName(b => b.OrgTypeCode, GetColumnNameCase())
                .HasMaxLength(50)
                .IsRequired()
                .HasComment("组织机构类型编码");

            builder.PropertyAndHasColumnName(b => b.StaffSizeCode, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("人员规模编码");

            builder.PropertyAndHasColumnName(b => b.AreaCode, GetColumnNameCase())
                .HasMaxLength(50)
                .IsRequired()
                .HasComment("所属行政区划编码");

            builder.PropertyAndHasColumnName(b => b.OrderNumber, GetColumnNameCase())
                .IsRequired()
                .HasComment("组织机构排序号");

            builder.PropertyAndHasColumnName(b => b.Leader, GetColumnNameCase())
                .HasMaxLength(50)
                .HasComment("机构负责人");

            builder.PropertyAndHasColumnName(b => b.Phone, GetColumnNameCase())
                .HasMaxLength(30)
                .HasComment("联系电话");

            builder.PropertyAndHasColumnName(b => b.Email, GetColumnNameCase())
                .HasMaxLength(100)
                .HasComment("联系邮箱");

            builder.PropertyAndHasColumnName(b => b.Address, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("所在地址");

            builder.PropertyAndHasColumnName(b => b.LogoUrl, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("Logo地址");

            builder.PropertyAndHasColumnName(b => b.Remarks, GetColumnNameCase())
                .HasMaxLength(500)
                .HasComment("描述");


            builder.PropertyAndHasColumnName(b => b.BusinessLicensePath, GetColumnNameCase())
                .HasMaxLength(200)
                .HasComment("营业执照路径");

            builder.PropertyAndHasColumnName(b => b.Confirmed, GetColumnNameCase())
                .IsRequired()
                .HasComment("组织机构认证状态");

            builder.PropertyAndHasColumnName(b => b.State, GetColumnNameCase())
                .IsRequired()
                .HasComment("组织机构状态");

            //设置索引
            builder.HasIndex(f => f.Code);
        }
    }
}