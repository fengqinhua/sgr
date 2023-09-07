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
using Sgr.Domain.Uow;
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
                sqlOptions
                    .MinBatchSize(1)
                    .MaxBatchSize(1000);

                //sqlOptions.CharSet(CharSet.Utf8);
                //sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);
                //// Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                //sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            };

            services.AddDbContext<SgrDbContext>(options =>
            {
                var serverVersion = ServerVersion.Parse(configuration.GetRequiredString("ConnectionStrings:SgrDBVersion"));

                options.UseMySql(configuration.GetRequiredString("ConnectionStrings:SgrDB"), serverVersion, ConfigureSqlOptions);
            });

            services.AddScoped<IUnitOfWork>(sp => { return sp.GetRequiredService<SgrDbContext>(); });

            return services;
        }
    }
}
