/**************************************************************
 * 
 * 唯一标识：8dfef932-34c5-4f5f-a890-44d8cb7b0dbd
 * 命名空间：Sgr.Generator
 * 创建时间：2023/7/28 16:34:15
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
    /// 雪花算法配置参数
    /// </summary>
    public class SnowflakeOption
    {
        /// <summary>
        /// 数据中心Id（可以用0、1、2、3、....31这32个数字）
        /// </summary>
        public long DatacenterId { get; set; } = 0;
        /// <summary>
        /// 计算机Id（可以用0、1、2、3、....31这32个数字）
        /// </summary>
        public long MachineId { get; set; } = 0;
    }
}
