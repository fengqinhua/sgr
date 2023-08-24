/**************************************************************
 * 
 * 唯一标识：6b9eae53-18dc-4990-8b33-a660ebf6a06c
 * 命名空间：Sgr.Domain.Repositories
 * 创建时间：2023/8/3 16:11:10
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Sgr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.Domain.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class EfCoreTreeNodeBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> :
        EfCoreRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey>, ITreeNodeBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, ITreeNode<TPrimaryKey>, IAggregateRoot
    {

        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetRootNodesAsync(
           CancellationToken cancellationToken = default)
        {
            var id = default(TPrimaryKey)!;
            return await GetChildNodesAsync(id,cancellationToken);
        }

        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetRootNodesAsync(
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default)
        {
            var id = default(TPrimaryKey)!;
            return await GetChildNodesAsync(id, propertiesToInclude,cancellationToken);
        }

        /// <summary>
        /// 是否存在子节点
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> HasChildNodesAsync(TEntity entity,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                return false;
            else
                return await GetQueryable().AnyAsync(f => f.ParentId!.Equals(entity.Id), cancellationToken);
        }

        /// <summary>
        /// 获取其子节点集合
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetChildNodesAsync(TEntity entity,
            CancellationToken cancellationToken = default)
        {
            if (entity == null)
                return Enumerable.Empty<TEntity>();
            else
                return await GetChildNodesAsync(entity!.Id, cancellationToken);
        }

        /// <summary>
        /// 获取其子节点集合
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetChildNodesAsync(TEntity entity,
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default)
        {

            if (entity == null)
                return Enumerable.Empty<TEntity>();
            else
                return await GetChildNodesAsync(entity!.Id, propertiesToInclude, cancellationToken);
        }

        /// <summary>
        /// 递归获取所有其子节点集合（含孙子节点）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetChildNodesRecursionAsync(TEntity entity,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .Where(f=>EF.Functions.Like(f.NodePath, $"{entity.NodePath}%"))
                //.Where(f => f.NodePath.StartsWith(entity.NodePath))
                .OrderBy(f => f.Id)
                .ToArrayAsync(cancellationToken);
        }

        /// <summary>
        /// 递归获取所有其子节点集合（含孙子节点）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetChildNodesRecursionAsync(TEntity entity,
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(propertiesToInclude)
                .Where(f => EF.Functions.Like(f.NodePath, $"{entity.NodePath}%"))
                //.Where(f => f.NodePath.StartsWith(entity.NodePath))
                .OrderBy(f => f.Id)
                .ToArrayAsync(cancellationToken);

        }


        /// <summary>
        /// 根据Id获取其子节点集合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetChildNodesAsync(TPrimaryKey id,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .Where(f => f.ParentId!.Equals(id))
                .OrderBy(f => f.Id)
                .ToArrayAsync(cancellationToken);
        }


        /// <summary>
        /// 根据Id获取其子节点集合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetChildNodesAsync(TPrimaryKey id,
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default)
        { 
            return await GetQueryable(propertiesToInclude)
                .Where(f => f.ParentId!.Equals(id))
                .OrderBy(f => f.Id)
                .ToArrayAsync(cancellationToken);
        }


        /// <summary>
        /// 根据Id递归获取所有其子节点集合（含孙子节点）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetChildNodesRecursionAsync(TPrimaryKey id,
            CancellationToken cancellationToken = default)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity == null)
                return Enumerable.Empty<TEntity>();
            else
                return await GetChildNodesRecursionAsync(entity!, cancellationToken);
        }


        /// <summary>
        /// 根据Id递归获取所有其子节点集合（含孙子节点）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetChildNodesRecursionAsync(TPrimaryKey id,
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity == null)
                return Enumerable.Empty<TEntity>();
            else
                return await GetChildNodesRecursionAsync(entity!, propertiesToInclude, cancellationToken);
        }
         

    }
}
