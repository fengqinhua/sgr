/**************************************************************
 * 
 * 唯一标识：4144705f-142a-48f3-b138-19a1c617718b
 * 命名空间：Sgr.Domain.Repositories
 * 创建时间：2023/8/3 13:08:51
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
using System.Threading.Tasks;
using System.Threading;

namespace Sgr.Domain.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface ITreeNodeBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> : IBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, ITreeNode<TPrimaryKey>, IAggregateRoot
    {
        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetRootNodesAsync(
           CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetRootNodesAsync(
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 是否存在子节点
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> HasChildNodesAsync(TEntity entity,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取其子节点集合
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetChildNodesAsync(TEntity entity,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取其子节点集合
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetChildNodesAsync(TEntity entity,
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 递归获取所有其子节点集合（含孙子节点）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetChildNodesRecursionAsync(TEntity entity,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 递归获取所有其子节点集合（含孙子节点）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetChildNodesRecursionAsync(TEntity entity,
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 根据Id获取其子节点集合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetChildNodesAsync(TPrimaryKey id,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 根据Id获取其子节点集合
        /// </summary>
        /// <param name="id"></param>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetChildNodesAsync(TPrimaryKey id,
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 根据Id递归获取所有其子节点集合（含孙子节点）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetChildNodesRecursionAsync(TPrimaryKey id,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 根据Id递归获取所有其子节点集合（含孙子节点）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetChildNodesRecursionAsync(TPrimaryKey id,
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default);
    }
}
