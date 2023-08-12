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
using System.Reflection;
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
        /// 获取数据库字段名称大小写设置规则
        /// </summary>
        /// <returns></returns>
        protected virtual ColumnNameCase GetColumnNameCase()
        {
            return ColumnNameCase.Lowercase;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var entityType = typeof(TEntity);

            builder.HasKey(b => b.Id);

            builder.PropertyAndHasColumnName("Id", GetColumnNameCase(), "sgr")
                .HasComment("主键");

            if (typeof(ITreeNode<TPrimaryKey>).IsAssignableFrom(entityType))
            {
                builder.PropertyAndHasColumnName("ParentId", GetColumnNameCase(), "sgr")
                    .IsRequired()
                    .HasComment("上级节点Id");

                builder.PropertyAndHasColumnName("NodePath", GetColumnNameCase(), "sgr")
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("树节点层次目录");


                builder.HasIndex("ParentId");
                builder.HasIndex("NodePath");
            }

            if (typeof(IExtendableObject).IsAssignableFrom(entityType))
            {
                builder.PropertyAndHasColumnName("ExtensionData", GetColumnNameCase(), "sgr")
                    .HasMaxLength(Constant.ExtendableObjectMaxLength)
                    .HasComment("扩展对象/实体");
            }

            if (typeof(IOptimisticLock).IsAssignableFrom(entityType))
            {
                builder.PropertyAndHasColumnName("RowVersion", GetColumnNameCase(), "sgr")
                    .IsConcurrencyToken()
                    .IsRequired()
                    //.HasValueGenerator<LongRowVersionValueGenerator>()
                    //.ValueGeneratedOnAddOrUpdate()
                    .HasComment("行版本");
            }

            if (typeof(IDomainEventManage).IsAssignableFrom(entityType))
            {
                builder.Ignore("DomainEvents");
            }

            if (typeof(ISoftDelete).IsAssignableFrom(entityType))
            {
                builder.PropertyAndHasColumnName("IsDeleted", GetColumnNameCase(), "sgr")
                    .IsRequired()
                    .HasComment("是否已经被软删除");
            }

            if(IsAssignableToOpenGenericType(entityType, typeof(IMustHaveOrg<>)))
            {
                builder.PropertyAndHasColumnName("OrgId", GetColumnNameCase(), "sgr")
                    .IsRequired()
                    .HasComment("所在组织ID");

                builder.HasIndex("OrgId");
            }

            if (IsAssignableToOpenGenericType(entityType, typeof(ICreationAudited<>)))
            {
                builder.PropertyAndHasColumnName("CreatorUserId", GetColumnNameCase(), "sgr")
                    .HasComment("创建的用户ID");

                builder.PropertyAndHasColumnName("CreationTime", GetColumnNameCase(), "sgr")
                    .HasComment("创建时间");
            }

            if (IsAssignableToOpenGenericType(entityType, typeof(ICreationAndModifyAudited<>)))
            {
                builder.PropertyAndHasColumnName("LastModifierUserId", GetColumnNameCase(), "sgr")
                    .HasComment("修改的用户ID");

                builder.PropertyAndHasColumnName("LastModificationTime", GetColumnNameCase(), "sgr")
                    .HasComment("修改时间");
            }

            if (IsAssignableToOpenGenericType(entityType, typeof(IFullAudited<>)))
            {
                builder.PropertyAndHasColumnName("DeleterUserId", GetColumnNameCase(), "sgr")
                    .HasComment("删除的用户ID");

                builder.PropertyAndHasColumnName("DeletionTime", GetColumnNameCase(), "sgr")
                    .HasComment("删除时间");
            }
        }

        private static bool IsAssignableToOpenGenericType(Type givenType, Type genericType)
        {
            return Array.Exists(givenType.GetInterfaces(), t => t.IsGenericType && t.GetGenericTypeDefinition() == genericType);
            
            //var interfaceTypes = givenType.GetInterfaces();
            //foreach (var it in interfaceTypes)
            //{
            //    if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
            //        return true;
            //}

            //if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            //    return true;

            //Type? baseType = givenType.BaseType;
            //if (baseType == null) return false;

            //return IsAssignableToGenericType(baseType, genericType);
        }
    }
}
