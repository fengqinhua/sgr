/**************************************************************
 * 
 * 唯一标识：daf62161-4dc2-4b09-9079-567336459a4f
 * 命名空间：Sgr.UPMS.API
 * 创建时间：2023/8/20 17:48:52
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sgr.AspNetCore.Modules;
using Sgr.EntityFrameworkCore;
using Sgr.UPMS.Domain.Departments;
using Sgr.UPMS.Domain.Duties;
using Sgr.UPMS.Domain.LogLogins;
using Sgr.UPMS.Domain.Organizations;
using Sgr.UPMS.Domain.Roles;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Infrastructure;
using Sgr.UPMS.Infrastructure.Checkers;
using Sgr.UPMS.Infrastructure.Repositories;

namespace Sgr.UPMS.API
{
    public class ModuleStartup : ModuleStartupBase
    {
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<ILogLoginRepository, LogLoginRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDutyRepository, DutyRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ILogLoginRepository, LogLoginRepository>();

            services.AddScoped<IOrganizationChecker, OrganizationChecker>();

            services.AddScoped<IOrganizationManage, OrganizationManage>();
            services.AddScoped<IDutyManage, DutyManage>();

            EntityFrameworkTypeRegistrar.Instance.Register<SgrDbContext, UPMSEntityFrameworkTypeProvider>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<ModuleStartup>();
            });

            
        }
    }
}
