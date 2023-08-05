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
using Sgr.EntityConfigurations;
using Sgr.EntityFrameworkCore;
using Sgr.OrganizationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            modelBuilder.Entity<Organization>();
        }

        /// <summary>
        /// 注册Configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        public void RegisterEntityConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
        }
    }
}
