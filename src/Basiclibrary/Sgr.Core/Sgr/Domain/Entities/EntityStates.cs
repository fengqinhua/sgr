/**************************************************************
 * 
 * 唯一标识：55108ab1-55ef-443e-9a8c-ff4a8542c893
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/8/3 8:44:49
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sgr.Domain.Entities
{
    /// <summary>
    /// 实体状态 枚举
    /// </summary>
    public enum EntityStates
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,
        /// <summary>
        /// 停用
        /// </summary>
        [Description("停用")]
        Deactivate
    }
}
