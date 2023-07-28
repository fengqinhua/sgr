/**************************************************************
 * 
 * 唯一标识：ee090857-486e-4c01-b376-5843fc8d3a26
 * 命名空间：Sgr.Generator
 * 创建时间：2023/7/28 16:28:38
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
    /// 长整型唯一标识生成器
    /// </summary>
    public interface INumberIdGenerator
    {
        /// <summary>
        /// 生成不重复的长整型唯一标识
        /// </summary>
        /// <returns></returns>
        long GenerateUniqueId();
    }
}
