/**************************************************************
 * 
 * 唯一标识：e30e4e87-77b7-46c3-8c3f-bd193c51df81
 * 命名空间：Microsoft.Extensions.DependencyInjection
 * 创建时间：2023/7/24 10:54:21
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Configuration;
using Sgr.Application;
using Sgr.AuditLogs.Services;
using Sgr.Caching.Services;
using Sgr.Database;
using Sgr.DataCategories.Services;
using Sgr.Domain.Entities.Auditing;
using Sgr.Generator;
using Sgr.Identity.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSgrCore(this IServiceCollection services, IConfiguration configuration)
        {
            //配置Id生成器
            services.Configure<SnowflakeOption>(configuration.GetSection("Maple:Snowflake"));
            services.AddSingleton<IStringIdGenerator, DefaultStringIdGenerator>();
            services.AddSingleton<INumberIdGenerator, DefaultNumberIdGenerator>();

            //数据源相关
            //services.AddSingleton<IDatabaseSourceInfoProvider, DefaultDatabaseSourceInfoProvider>();
            //services.AddSingleton<IDatabaseSourceInfoManager, DefaultDatabaseSourceInfoManager>();
            services.AddTransient<IDatabaseSeed, DefaultDatabaseSeed>();
            
            //数据字典
            services.AddSingleton<ICategoryTypeService, DefaultCategoryTypeService>();


            //签名验证工具
            services.AddSingleton<ISignatureChecker, DefaultSignatureChecker>();

            //配置审计接口
            services.AddSingleton<IAuditedOperator, DefaultAuditedOperator>();
            services.AddTransient<IAuditLogService, DefaultAuditLogService>();

            //认证相关
            services.AddScoped<IAccountService, NoAccountService>();
            services.AddScoped<IRoleService, NoRoleService>();
            //services.AddTransient<IPermissionChecker, PermissionChecker>();

            //缓存
            services.AddSingleton<ICacheManager, NoCacheManager>();
            

            return services;
        }

         


    }
}
