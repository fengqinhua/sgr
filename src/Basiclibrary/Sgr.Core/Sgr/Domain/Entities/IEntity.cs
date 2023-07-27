/**************************************************************
 * 
 * 唯一标识：9ee43a4b-c31b-4dbb-bb31-f85bc6ecde21
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/7/25 11:31:00
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Domain.Entities
{
    /// <summary>
    /// 定义实体类型的接口。系统中的所有实体都必须实现此接口。
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键ID类型</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// ID ，实体的唯一标识符
        /// </summary>
        TPrimaryKey Id { get; set; }
        /// <summary>
        /// 检查此实体是否是暂时的（未持久化到数据库）
        /// </summary>
        /// <returns></returns>
        bool IsTransient(); 
    }

}
