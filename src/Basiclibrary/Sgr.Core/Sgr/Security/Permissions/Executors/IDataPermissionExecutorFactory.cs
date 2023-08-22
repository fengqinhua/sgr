/**************************************************************
 * 
 * 唯一标识：4030b90d-6f6e-4a5a-b037-6c1fca458821
 * 命名空间：Sgr.Security.Permissions.Executors
 * 创建时间：2023/8/22 16:52:30
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Security.Permissions.Executors
{
    public interface IDataPermissionExecutorFactory
    {
        IDataPermissionExecutor<TEntity> Create<TEntity>(string name) where TEntity : class, IAggregateRoot;
    }
}
