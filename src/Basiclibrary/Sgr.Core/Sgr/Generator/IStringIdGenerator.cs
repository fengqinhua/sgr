/**************************************************************
 * 
 * 唯一标识：2055bda3-4818-4b5d-8ef9-8be675dd0805
 * 命名空间：Sgr.Generator
 * 创建时间：2023/7/28 16:29:16
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Generator
{
    /// <summary>
    /// 字符串型唯一标识生成器
    /// </summary>
    public interface IStringIdGenerator
    {
        /// <summary>
        /// 生成不重复的字符串型唯一标识
        /// </summary>
        /// <returns></returns>
        string GenerateUniqueId();
    }
}
