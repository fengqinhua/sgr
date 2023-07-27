/**************************************************************
 * 
 * 唯一标识：d82fb870-56e0-407c-99ae-f1d65f02b8d4
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/7/27 14:04:33
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
    /// 包含领域事件的实体对象
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntityHaveDomainEvent<TPrimaryKey> : IEntity<TPrimaryKey>, IDomainEventManage
    {

    }
}
