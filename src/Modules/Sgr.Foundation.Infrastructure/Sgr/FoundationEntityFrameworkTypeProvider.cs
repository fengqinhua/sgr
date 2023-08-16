/**************************************************************
 * 
 * 唯一标识：430e793b-4eae-44fd-9bf8-7e9695015776
 * 命名空间：Sgr
 * 创建时间：2023/8/4 16:41:04
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Sgr.AuditLogAggregate;
using Sgr.DataDictionaryAggregate;
using Sgr.DepartmentAggregate;
using Sgr.DutyAggregate;
using Sgr.EntityConfigurations;
using Sgr.EntityFrameworkCore;
using Sgr.OrganizationAggregate;
using Sgr.RoleAggregate;
using Sgr.UserAggregate;
using System;

namespace Sgr
{
    /// <summary>
    /// 
    /// </summary>
    public class FoundationEntityFrameworkTypeProvider : IEntityFrameworkTypeProvider
    {
        /// <summary>
        /// 注册Entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RegisterEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogLogin>();
            modelBuilder.Entity<LogOperate>();

            modelBuilder.Entity<Organization>();
            modelBuilder.Entity<Department>();
            modelBuilder.Entity<Duty>();

            modelBuilder.Entity<Role>();
            modelBuilder.Entity<RoleResource>();

            modelBuilder.Entity<User>();
            modelBuilder.Entity<UserDuty>();
            modelBuilder.Entity<UserRole>();

            modelBuilder.Entity<DataCategoryItem>();

        }

        /// <summary>
        /// 注册Configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void RegisterEntityConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogLoginEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LogOperateEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DutyEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleResourceEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserDutyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new DataCategoryItemConfiguration());
        }
    }
}
