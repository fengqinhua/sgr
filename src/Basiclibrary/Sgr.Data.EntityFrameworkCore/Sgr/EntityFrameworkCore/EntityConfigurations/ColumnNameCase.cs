/**************************************************************
 * 
 * 唯一标识：790d2f98-e95d-46c0-acf5-530cd715d6e2
 * 命名空间：Sgr.EntityFrameworkCore.EntityConfigurations
 * 创建时间：2023/8/6 10:08:10
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 大小写设置
    /// </summary>
    public enum ColumnNameCase
    {
        /// <summary>
        /// 保持不变
        /// </summary>
        Remaining,
        /// <summary>
        /// 转大写
        /// </summary>
        Uppercase,
        /// <summary>
        /// 转小写
        /// </summary>
        Lowercase
    }

}
