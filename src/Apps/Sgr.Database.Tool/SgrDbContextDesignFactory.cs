/**************************************************************
 * 
 * 唯一标识：9b8c93d0-73d6-44a9-9a77-970400d8ccb5
 * 命名空间：Sgr.Database.Tool
 * 创建时间：2023/8/4 21:37:05
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sgr.EntityFrameworkCore;
using MediatR;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Sgr.Database.Tool
{
    /// <summary>
    /// 
    /// </summary>
    public class SgrDbContextDesignFactory : IDesignTimeDbContextFactory<SgrDbContext>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public SgrDbContext CreateDbContext(string[] args)
        {
            //Debugger.Launch();

            //var config = new ConfigurationBuilder()
            //   .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            //   .AddJsonFile("appsettings.json")
            //   .AddEnvironmentVariables()
            //   .Build();
            //config["ConnectionString"]

            var serverVersion = new MySqlServerVersion(new Version(5, 7, 10));
            var optionsBuilder = new DbContextOptionsBuilder<SgrDbContext>()
                .UseMySql("server=localhost;port=3306;database=map_sam;uid=root;pwd=1qaz@WSX;",
                serverVersion,
                b => b.MigrationsAssembly("Sgr.Database.Tool"));

            var dbContext = new SgrDbContext(optionsBuilder.Options, new NoMediator()); 
            return dbContext;
        }
    }
}
