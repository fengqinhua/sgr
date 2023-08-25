using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Sgr.Modules
{
    /// <summary>
    /// 用于初始化模块的服务和HTTP请求管道的基类
    /// </summary>
    public abstract class ModuleStartupBase : IModuleStartup
    {
        /// <summary>
        /// 模块服务初始化顺序，默认为0
        /// </summary>
        public virtual int Order => 1000;
        /// <summary>
        /// 模块服务初始化
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }

        /// <summary>
        /// 初始化HTTP请求管道初始化
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void ConfigureServices(IServiceCollection services)
        {

        }
    }
}
