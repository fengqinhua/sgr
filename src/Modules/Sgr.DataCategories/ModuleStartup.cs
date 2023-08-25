/**************************************************************
 * 
 * 唯一标识：9dd0dace-5508-44ce-998f-289b2bfeb2fa
 * 命名空间：Sgr.DataCategories
 * 创建时间：2023/8/20 16:49:55
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sgr.AuditLogs;
using Sgr.Database;
using Sgr.DataCategories.Application.Commands;
using Sgr.DataCategories.Application.Queries;
using Sgr.DataCategories.Application.Validations;
using Sgr.DataCategories.Domain;
using Sgr.DataCategories.Infrastructure;
using Sgr.DataCategories.Infrastructure.Checkers;
using Sgr.DataCategories.Infrastructure.Repositories;
using Sgr.DataCategories.Infrastructure.Sead;
using Sgr.DataCategories.Services;
using Sgr.EntityFrameworkCore;
using Sgr.Modules;

namespace Sgr.DataCategories
{
    public class ModuleStartup : ModuleStartupBase
    {
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICategoryTypeProvider, CategoryTypeProvider>();

            services.AddScoped<IDataCategoryQueries, DataCategoryQueries>();

            services.AddScoped<IDataCategoryItemRepository, DataCategoryItemRepository>();
            services.AddScoped<IDataCategoryItemChecker, DataCategoryItemChecker>();
            services.AddScoped<IDataCategoryItemManage, DataCategoryItemManage>();

            //services.AddTransient<IDataBaseInitialize, DataCategoryItemAreaDataBaseInitialize>();
            services.AddTransient<IDataBaseInitialize, DataCategoryItemDataBaseInitialize>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<ModuleStartup>();
            });

            services.AddTransient<IValidator<CreateCategpryItemCommand>, CreateCategpryItemCommandValidator>();

            EntityFrameworkTypeRegistrar.Instance.Register<SgrDbContext, DataCategoriesEntityFrameworkTypeProvider>();
        }
    }
}