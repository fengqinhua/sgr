﻿/**************************************************************
 * 
 * 唯一标识：93de1631-661c-4c84-b470-498d43267416
 * 命名空间：Sgr.IntegrationEvents
 * 创建时间：2023/8/19 21:13:34
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sgr.Modules;

namespace Sgr.IntegrationEvents
{
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

        }
    }
}
