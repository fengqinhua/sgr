/**************************************************************
 * 
 * 唯一标识：d391481b-9630-4296-b485-b227b9d99a20
 * 命名空间：Sgr.Database.Tool.Extensions
 * 创建时间：2023/8/4 21:25:46
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Sgr.EntityFrameworkCore;
using System;

namespace Sgr.Database.Tool.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            static void ConfigureSqlOptions(MySqlDbContextOptionsBuilder sqlOptions)
            {
                //sqlOptions.CharSet(CharSet.Utf8);

                //sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);

                //// Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 

                //sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            };

            services.AddDbContext<SgrDbContext>(options =>
            {
                //Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql")
                var serverVersion = new MySqlServerVersion(new Version(5, 7, 10));
                options.UseMySql("server=localhost;port=3306;database=map_sam;uid=root;pwd=1qaz@WSX;", serverVersion, ConfigureSqlOptions);

            });

            //services.AddDbContext<SgrDbContext>((sp,options) =>
            //{
            //    var serverVersion = new MySqlServerVersion(new Version(5, 7, 10));

            //    options.UseMySql("server=localhost;port=3306;database=map_sam;uid=root;pwd=root;CharSet=utf8;", serverVersion, ConfigureSqlOptions)
            //        .UseInternalServiceProvider(sp);
            //    //options.UseMySql(configuration.GetRequiredConnectionString("OrderingDB"), ConfigureSqlOptions);
            //});

            //services.AddDbContext<IntegrationEventLogContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetRequiredConnectionString("OrderingDB"), ConfigureSqlOptions);
            //});

            return services;
        }
    }
}
