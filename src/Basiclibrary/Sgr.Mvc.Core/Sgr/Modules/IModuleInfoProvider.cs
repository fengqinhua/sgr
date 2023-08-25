/**************************************************************
 * 
 * 唯一标识：e413dd6a-b43f-4a81-b930-e3adc7218529
 * 命名空间：Sgr.Modules
 * 创建时间：2023/7/30 9:58:12
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
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Modules
{
    /// <summary>
    /// 模块信息提供者
    /// </summary>
    public interface IModuleInfoProvider
    {
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        IEnumerable<ModuleInfo> GetModules(IHostEnvironment environment);
    }
}
