/**************************************************************
 * 
 * 唯一标识：4db3a281-ed25-4e9c-b3c8-7cc28127eb32
 * 命名空间：Sgr.Modules
 * 创建时间：2023/7/30 11:10:46
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Modules
{
    /// <summary>
    /// 该接口用于初始化模块的服务和HTTP请求管道
    /// </summary>
    public interface IModuleStartup
    {
        /// <summary>
        /// 模块服务初始化顺序，默认为0
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 模块服务初始化
        /// </summary>
        /// <param name="services"></param>
        void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// 初始化HTTP请求管道初始化
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}
