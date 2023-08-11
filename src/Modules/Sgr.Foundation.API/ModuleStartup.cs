﻿/**************************************************************
 * 
 * 唯一标识：09485f5f-18fb-4525-9ee1-93dc1d4ae165
 * 命名空间：Sgr.Foundation.API
 * 创建时间：2023/7/30 18:23:47
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sgr.Domain.Repositories;
using Sgr.EntityFrameworkCore;
using Sgr.Foundation.API.OrgEndpoints;
using Sgr.Foundation.API.Services;
using Sgr.OrganizationAggregate;
using Sgr.Services;
using Sgr.AspNetCore.Modules;
using Sgr.AuditLogAggregate;
using Sgr.DepartmentAggregate;
using Sgr.DutyAggregate;
using Sgr.RoleAggregate;
using Sgr.UserAggregate;

namespace Sgr.Foundation.API
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuleStartup : ModuleStartupBase
    {
        /// <summary>
        /// 初始化HTTP请求管道初始化
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }

        /// <summary>
        /// 模块服务初始化
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<ILogLoginRepository, LogLoginRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDutyRepository, DutyRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IOrganizationManage, OrganizationManage>();
            services.AddScoped<IDutyManage, DutyManage>();

            EntityFrameworkTypeRegistrar.Instance.Register<FoundationEntityFrameworkTypeProvider>();

            //services.Replace(ServiceDescriptor.Transient<IAuditLogService, AuditLogService>());
            //services.AddScoped<IEndpoint, OrgGetByIdEndpoint>();
        }
    }
}
