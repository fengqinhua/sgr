/**************************************************************
 * 
 * 唯一标识：5a31582f-d5ad-4c49-a2c5-462f6d17c06b
 * 命名空间：Sgr.Modules
 * 创建时间：2023/7/30 10:57:05
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.AspNetCore.Modules
{
    /// <summary>
    /// 模块信息提供者(缺省的)
    /// <para>在应用程序跟目录下寻找*.ModuleManifest.json后缀的配置文件，从而获得模块信息</para>
    /// </summary>
    public sealed class AppConfigurationModuleInfoProvider : ConfigurationModuleInfoProviderBase
    {
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        protected override string GetModuleDirectory(IHostEnvironment environment)
        {
            return AppDomain.CurrentDomain.BaseDirectory;
            //return environment.ContentRootPath;
        }
    }
}
