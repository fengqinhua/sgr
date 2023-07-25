/**************************************************************
 * 
 * 唯一标识：1b060989-05c8-4e7d-85f6-62161239b4ef
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/7/25 11:32:49
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
    /// 乐观锁
    /// </summary>
    public interface IOptimisticLock
    {
        /// <summary>
        /// 行版本
        /// </summary>
        long RowVersion { get; set; }
    }
}
