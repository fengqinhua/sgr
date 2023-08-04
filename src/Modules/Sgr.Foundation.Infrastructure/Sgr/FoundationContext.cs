/**************************************************************
 * 
 * 唯一标识：c1946355-b497-4d62-96e4-1fb646ff9ef8
 * 命名空间：Sgr
 * 创建时间：2023/8/4 9:03:44
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgr.EntityConfigurations;
using Sgr.EntityFrameworkCore;
using Sgr.OrganizationAggregate;

namespace Sgr
{
    /// <summary>
    /// efcore 数据库访问上下文
    /// </summary>
    public class FoundationContext : UnitOfWorkDbContext
    {
        /// <summary>
        /// 缺省的 Schema
        /// </summary>
        public const string DEFAULT_SCHEMA = "sgr";

        /// <summary>
        /// 组织机构
        /// </summary>
        public DbSet<Organization> Organizations { get; set; }

        /// <summary>
        /// efcore 数据库访问上下文
        /// </summary>
        /// <param name="options"></param>
        /// <param name="mediator"></param>
        public FoundationContext(DbContextOptions<FoundationContext> options, IMediator mediator) 
            : base(options, mediator)
        {
        }

        /// <summary>
        /// 配置模型定义
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
        }



    }
}
