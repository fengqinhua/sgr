/**************************************************************
 * 
 * 唯一标识：f17cc66d-be6b-4446-95db-49c243d125f1
 * 命名空间：Sgr.Security.Permissions
 * 创建时间：2023/8/22 16:01:33
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 * 验证数据权限的具体执行者
 *  IsSatisfiedBy ：检查当前聚合根是否符合数据权限规范要求
 *  ToExpression  ：按照规则返回符合数据权限要求的数据检索条件
 **************************************************************/

using Sgr.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Sgr.Security.Permissions.Executors
{
    public interface IDataPermissionExecutor<TEntity>
        where TEntity : class, IAggregateRoot
    {
        bool IsSatisfiedBy(TEntity obj);
        Expression<Func<TEntity, bool>> ToExpression();
    }
}
