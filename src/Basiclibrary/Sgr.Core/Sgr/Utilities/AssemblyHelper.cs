/**************************************************************
 * 
 * 唯一标识：52247a56-0d9a-4a0f-8f7e-00ab57aa9864
 * 命名空间：Sgr.Utilities
 * 创建时间：2023/8/5 11:44:37
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sgr.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class AssemblyHelper
    {
        //public static List<Assembly> LoadAssemblies(string folderPath, SearchOption searchOption)
        //{
        //    return GetAssemblyFiles(folderPath, searchOption)
        //        .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
        //        .ToList();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAssemblyFiles(string folderPath, SearchOption searchOption)
        {
            return Directory
                .EnumerateFiles(folderPath, "*.*", searchOption)
                .Where(s => s.EndsWith(".dll") || s.EndsWith(".exe"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IReadOnlyList<Type> GetAllTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types;
            }
        }
    }
}
