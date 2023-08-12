/**************************************************************
 * 
 * 唯一标识：ac5467f2-4cd0-412c-adbd-fb480e2158a2
 * 命名空间：Sgr.EntityFrameworkCore
 * 创建时间：2023/8/4 16:11:47
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgr.EntityFrameworkCore.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityFrameworkCore
{
    /// <summary>
    /// efcore 数据库访问上下文
    /// </summary>
    public class SgrDbContext : UnitOfWorkDbContext
    {
        /// <summary>
        /// efcore 数据库访问上下文
        /// </summary>
        /// <param name="options"></param>
        /// <param name="mediator"></param>
        public SgrDbContext(DbContextOptions<SgrDbContext> options, IMediator mediator) : base(options, mediator)
        { 

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
#if DEBUG
            Console.WriteLine("OnModelCreating *******************************************");
#endif

            base.OnModelCreating(modelBuilder);

            var types = EntityFrameworkTypeRegistrar.Instance.AllTypes;

            foreach (var type in types)
            {
                if (Activator.CreateInstance(type) is IEntityFrameworkTypeProvider typeProvider)
                {
                    typeProvider.RegisterEntities(modelBuilder);
                    typeProvider.RegisterEntityConfigurations(modelBuilder);
                }

            }

        }

    }
}
