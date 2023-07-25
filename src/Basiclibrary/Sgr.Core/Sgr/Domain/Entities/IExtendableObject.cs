/**************************************************************
 * 
 * 唯一标识：c79ce8c0-72aa-4b43-93fa-959cb3af8aca
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/7/25 11:33:25
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Domain.Entities
{
    /// <summary>
    /// 定义一个JSON格式的字符串属性来扩展对象/实体
    /// </summary>
    public interface IExtendableObject
    {
        /// <summary>
        /// 一个JSON格式化的字符串来扩展对象/实体
        /// JSON字符串以Key/Value的形式存在，格式如下：
        /// <code>
        /// {
        ///   "Property1" : ...
        ///   "Property2" : ...
        /// }
        /// </code>
        /// </summary>
        string? ExtensionData { get; set; }
    }
}
