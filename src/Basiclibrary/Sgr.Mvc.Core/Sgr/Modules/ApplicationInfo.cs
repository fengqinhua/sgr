/**************************************************************
 * 
 * 唯一标识：40ea9b76-cf3d-4fba-bcd5-6c39f999188d
 * 命名空间：Sgr.Modules
 * 创建时间：2023/7/30 9:20:11
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Modules
{
    /// <summary>
    /// 应用程序信息
    /// </summary>
    public class ApplicationInfo
    {
        private readonly Dictionary<string, ModuleInfo> _modulesById;
        private readonly List<ModuleInfo> _modules;
        
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 应用程序路径
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 应用程序Host程序集
        /// </summary>
        public Assembly Assembly { get; }
        /// <summary>
        /// 模块基本信息集合
        /// </summary>
        public IEnumerable<ModuleInfo> Modules => _modules;

        /// <summary>
        /// 应用程序信息
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="modules"></param>
        public ApplicationInfo(IHostEnvironment environment, IEnumerable<ModuleInfo> modules)
        {
            Name = environment.ApplicationName;
            Path = environment.ContentRootPath;

            Assembly = Assembly.Load(new AssemblyName(Name));
            _modules = new List<ModuleInfo>(modules);
            _modulesById = _modules.ToDictionary(m => m.Id, m => m);
        }

        /// <summary>
        /// 根据Id获取模块基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ModuleInfo GetModuleInfo(string id)
        {
            if (!_modulesById.TryGetValue(id, out var module))
                return new ModuleInfo();
            return module;
        }
    }
}
