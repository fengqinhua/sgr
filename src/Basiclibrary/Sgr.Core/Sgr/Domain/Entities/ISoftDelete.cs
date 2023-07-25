/**************************************************************
 * 
 * 唯一标识：dc699ac3-431f-41e2-b359-0c7b5c3f39dd
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/7/25 11:33:08
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
    /// 接口 ： 标识实体是否已经被软删除.
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 标识实体是否已经被软删除. 
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
