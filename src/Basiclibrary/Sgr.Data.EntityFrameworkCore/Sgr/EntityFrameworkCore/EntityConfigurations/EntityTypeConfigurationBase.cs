/**************************************************************
 * 
 * 唯一标识：4bf11f26-2ca8-4628-ab98-813e1a5572e6
 * 命名空间：Sgr.EntityFrameworkCore.EntityConfigurations
 * 创建时间：2023/8/4 9:21:40
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Sgr.Domain.Entities;
using Sgr.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class EntityTypeConfigurationBase<TEntity, TPrimaryKey>
         : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

            var entityType = typeof(TEntity);

            builder.HasKey(b => b.Id);
            builder.Property("Id")
                .ValueGeneratedNever()
                .HasComment("主键");


            if (typeof(ITreeNode<TPrimaryKey>).IsAssignableFrom(entityType))
            {
                builder.Property("ParentId")
                    .IsRequired()
                    .HasComment("上级节点Id");

                builder.Property("NodePath")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("树节点层次目录");
            }

            if (typeof(IExtendableObject).IsAssignableFrom(entityType))
            {
                builder.Property("ExtensionData")
                    .HasMaxLength(Constant.ExtendableObjectMaxLength)
                    .HasComment("扩展对象/实体");
            }

            if (typeof(IOptimisticLock).IsAssignableFrom(entityType))
            {
                builder.Property("RowVersion")
                    .IsConcurrencyToken()
                    .IsRequired()
                    .HasValueGenerator<LongValueGenerator>()
                    //.ValueGeneratedOnAddOrUpdate()
                    .HasComment("行版本");
            }

            if (typeof(IDomainEventManage).IsAssignableFrom(entityType))
            {
                builder.Ignore("DomainEvents");
            }

            if (typeof(ISoftDelete).IsAssignableFrom(entityType))
            {
                builder.Property("IsDeleted")
                    .IsRequired()
                    .HasComment("是否已经被软删除");
            }

            if (typeof(IMustHaveOrg<>).IsAssignableFrom(entityType))
            {
                builder.Property("OrgId")
                    .IsRequired()
                    .HasComment("所在组织ID");
            }

            if (typeof(ICreationAudited<>).IsAssignableFrom(entityType))
            {
                builder.Property("CreatorUserId")
                    .HasComment("创建的用户ID");

                builder.Property("CreationTime")
                    .HasComment("创建时间");
            }

            if (typeof(ICreationAndModifyAudited<>).IsAssignableFrom(entityType))
            {
                builder.Property("LastModifierUserId")
                    .HasComment("修改的用户ID");

                builder.Property("LastModificationTime")
                    .HasComment("修改时间");
            }

            if (typeof(IFullAudited<>).IsAssignableFrom(entityType))
            {
                builder.Property("DeleterUserId")
                    .HasComment("删除的用户ID");

                builder.Property("DeletionTime")
                    .HasComment("删除时间");
            }
        }
         
    }
}
