/**************************************************************
 * 
 * 唯一标识：e3e6f7c8-da6d-4f00-8778-5c13230affd1
 * 命名空间：Sgr.Modules
 * 创建时间：2023/7/30 7:39:07
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Sgr.AspNetCore.Modules
{
    /// <summary>
    /// 模块基本信息
    /// </summary>
    public class ModuleInfo : ModuleManifest
    {
        /// <summary>
        /// 模块版本
        /// </summary>
        public Version? Version { get; set; }
        /// <summary>
        /// 模块所在的应用程序集
        /// </summary>
        public Assembly? Assembly { get; set; }

        ///// <summary>
        ///// 模块是否仅是因依赖关系而需要加载
        ///// </summary>
        //public bool IsEnabledByDependencyOnly { get; set; } = false;
        ///// <summary>
        ///// 依赖的模块集合
        ///// </summary>
        //public virtual string[] Dependencies { get; set; } = new string[] { };
    }
}
