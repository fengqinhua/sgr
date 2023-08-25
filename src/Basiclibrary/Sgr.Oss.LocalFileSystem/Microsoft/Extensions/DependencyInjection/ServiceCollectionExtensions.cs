/**************************************************************
 * 
 * 唯一标识：c027b627-3fa9-4cd6-9d17-14f585c3f3f1
 * 命名空间：Microsoft.Extensions.DependencyInjection
 * 创建时间：2023/8/25 13:39:49
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Configuration;
using Sgr.Oss;
using Sgr.Oss.Services;
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
        public static IServiceCollection AddSgrLocalFileSystemOss(this IServiceCollection services,
            IConfiguration configuration)
        {
            LocalFilesystemOptions cookieOpt = configuration.GetSection("Sgr:Oss:LocalFileSystem").Get<LocalFilesystemOptions>() ?? LocalFilesystemOptions.CreatDefault();
            services.AddSingleton(cookieOpt);

            services.AddTransient<IOssService, LocalFilesystemOssService>();
            return services;
        }
    }
}