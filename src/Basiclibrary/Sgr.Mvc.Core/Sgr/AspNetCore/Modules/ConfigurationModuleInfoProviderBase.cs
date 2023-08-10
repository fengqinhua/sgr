/**************************************************************
 * 
 * 唯一标识：9ed5e0c8-1199-459f-b5f6-aa2053fd94d6
 * 命名空间：Sgr.Modules
 * 创建时间：2023/7/30 10:02:17
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using Microsoft.Extensions.Hosting;
using Sgr.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.AspNetCore.Modules
{
    /// <summary>
    /// 基于json配置获取模块信息（从指定目录中）
    /// </summary>
    public abstract class ConfigurationModuleInfoProviderBase : IModuleInfoProvider
    {
        /// <summary>
        /// 模块信息配置文件后缀名称
        /// </summary>
        public static readonly string ModulesFilename = "*.ModuleManifest.json";

        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public IEnumerable<ModuleInfo> GetModules(IHostEnvironment environment)
        {
            string modulesPath = GetModuleDirectory(environment);

            var files = Directory.GetFiles(modulesPath, ModulesFilename, SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                using var reader = new StreamReader(file);
                string content = reader.ReadToEnd();

                ModuleManifest? module = JsonHelper.DeserializeObject<ModuleManifest>(content);
                if (module != null && !string.IsNullOrEmpty(module.Id))
                {
                    var assemblyName = new AssemblyName(module.Id);
                    var assembly = Assembly.Load(assemblyName);
                    var version = assembly.GetName().Version;

                    yield return new ModuleInfo
                    {
                        Id = module.Id,
                        Name = module.Name,
                        Category = module.Category,
                        Description = module.Description,
                        IsEnable = module.IsEnable,
                        Assembly = assembly,
                        Version = version
                    };
                }
            }
        }

        /// <summary>
        /// 获取模块所在目录
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        protected abstract string GetModuleDirectory(IHostEnvironment environment);
    }
}
