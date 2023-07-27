/**************************************************************
 * 
 * 唯一标识：249ca3c7-5804-4786-861d-f9a3eaeebfdc
 * 命名空间：Sgr.Domain.Repositories
 * 创建时间：2023/7/26 15:55:29
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
    /// 支持基础写入和查询功能的仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> : IReadOnlyBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {

        #region INSERT
        /// <summary>
        /// 创建一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// 创建多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion

        #region UPDATE
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion

        #region DELETE

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default);
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BulkDeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BulkDeleteAsync(IEnumerable<TPrimaryKey> ids, CancellationToken cancellationToken = default);

        #endregion

    }
}
