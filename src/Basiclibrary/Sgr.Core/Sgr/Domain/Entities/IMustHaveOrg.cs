/**************************************************************
 * 
 * 唯一标识：65409fa3-7ea7-48f8-ba6e-99152c9804d9
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/7/25 11:31:51
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
    /// 如果一个实体实现该接口，那么必须存储实体所在组织的ID.
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IMustHaveOrg<TPrimaryKey>
    {
        /// <summary>
        /// 实体所在组织ID.
        /// </summary>
        TPrimaryKey OrgId { get; }
    }
}
