/**************************************************************
 * 
 * 唯一标识：93ff6798-f6d6-47ca-ad24-10a8498fd549
 * 命名空间：Sgr.Modules
 * 创建时间：2023/7/30 9:56:13
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Sgr.Modules
{
    /// <summary>
    /// 基于模块的应用程序上下文
    /// </summary>
    public class ModularApplicationInfoContext : IApplicationInfoContext
    {
        private readonly IHostEnvironment _environment;
        private readonly IModuleInfoProvider _moduleInfoProvider;
        private ApplicationInfo? _application;
        private static readonly object _initLock = new();

        /// <summary>
        /// 基于模块的应用程序上下文
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="moduleInfoProvider"></param>
        public ModularApplicationInfoContext(IHostEnvironment environment,
            IModuleInfoProvider moduleInfoProvider)
        {
            _environment = environment;
            _moduleInfoProvider = moduleInfoProvider;
        }

        /// <summary>
        /// 应用程序信息
        /// </summary>
        public ApplicationInfo Application
        {
            get
            {
                EnsureInitialized();
                return _application!;
            }
        }

        private void EnsureInitialized()
        {
            if (_application == null)
            {
                lock (_initLock)
                {
                    _application ??= new ApplicationInfo(_environment, GetModules());
                }
            }
        }

        private IEnumerable<ModuleInfo> GetModules()
        {


            return _moduleInfoProvider
                .GetModules(_environment)
                //.SelectMany(f=>f.GetModules(_environment))
                .Where(n => n.Id != _environment.ApplicationName)
                .Distinct();

            //Parallel.ForEach(names, new ParallelOptions { MaxDegreeOfParallelism = 8 }, (name) =>
            //{
            //    
            //});
        }
    }
}
