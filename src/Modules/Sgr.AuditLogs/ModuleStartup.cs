/**************************************************************
 * 
 * 唯一标识：9d053614-c48f-41d7-9321-c731b33eddd3
 * 命名空间：Sgr.AuditLogs
 * 创建时间：2023/8/20 15:42:59
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sgr.AuditLogs.Infrastructure;
using Sgr.AuditLogs.Queries;
using Sgr.AuditLogs.Services;
using Sgr.EntityFrameworkCore;
using Sgr.Modules;

namespace Sgr.AuditLogs
{
    public class ModuleStartup : ModuleStartupBase
    {
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Transient<IAuditLogService, AuditLogService>());

            services.AddTransient<ILogOperateQueries, LogOperateQueries>();

            EntityFrameworkTypeRegistrar.Instance.Register<SgrDbContext, AuditLogsEntityFrameworkTypeProvider>();


        }
    }
}
