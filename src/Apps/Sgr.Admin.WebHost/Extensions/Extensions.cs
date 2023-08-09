/**************************************************************
 * 
 * 唯一标识：25dec45c-1ea3-422b-8f57-7f6718b72151
 * 命名空间：Sgr.Admin.WebHost.Extensions
 * 创建时间：2023/8/9 16:54:11
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
using Sgr.EntityFrameworkCore;
using System;

namespace Sgr.Admin.WebHost.Extensions
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
