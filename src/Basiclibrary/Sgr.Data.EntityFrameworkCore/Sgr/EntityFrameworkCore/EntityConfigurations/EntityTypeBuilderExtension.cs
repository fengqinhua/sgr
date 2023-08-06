/**************************************************************
 * 
 * 唯一标识：fbecb5a1-78a2-44bb-9203-e4e6ee369dc4
 * 命名空间：Sgr.EntityFrameworkCore.EntityConfigurations
 * 创建时间：2023/8/6 10:09:08
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 
    /// </summary>
    public static class EntityTypeBuilderExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="propertyName"></param>
        /// <param name="columnNameCase"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static PropertyBuilder PropertyAndHasColumnName(this EntityTypeBuilder builder,
            string propertyName,
            ColumnNameCase columnNameCase = ColumnNameCase.Lowercase,
            string prefix = "m")
        {
            var propertyBuilder = builder.Property(propertyName);

            return propertyBuilder.HasColumnName(getColumnName(propertyName, prefix, columnNameCase));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="builder"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="columnNameCase"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static PropertyBuilder<TProperty> PropertyAndHasColumnName<TEntity, TProperty>(this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            ColumnNameCase columnNameCase = ColumnNameCase.Lowercase,
            string prefix = "m") where TEntity : class
        {
            var propertyBuilder = builder.Property(propertyExpression);
            return propertyBuilder.HasColumnName(getColumnName(propertyBuilder.Metadata.GetColumnName(), prefix, columnNameCase));
        }



        private static string getColumnName(string propertyName, string prefix, ColumnNameCase columnNameCase)
        {
            string columnName = string.IsNullOrEmpty(prefix) ? propertyName : $"{prefix}_{propertyName}";
            switch (columnNameCase)
            {
                case ColumnNameCase.Lowercase:
                    columnName = columnName.ToLower();
                    break;
                case ColumnNameCase.Uppercase:
                    columnName = columnName.ToUpper();
                    break;
                case ColumnNameCase.Remaining:
                default:
                    break;
            }

            return columnName;
        }
    }
}
