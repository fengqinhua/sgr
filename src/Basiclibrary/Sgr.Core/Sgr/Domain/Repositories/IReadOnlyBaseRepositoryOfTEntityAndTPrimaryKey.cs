/**************************************************************
 * 
 * 唯一标识：5b3283ca-a802-469d-a269-9f4038da2c24
 * 命名空间：Sgr.Domain.Repositories
 * 创建时间：2023/7/26 15:45:06
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
    /// 支持基础查询功能的仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IReadOnlyBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> : IRepository<TEntity>
        where TEntity : class, IEntity<TPrimaryKey>, IAggregateRoot
    {
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> CountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据对象全局唯一标识获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity?> GetAsync(TPrimaryKey id,
            bool includeDetails = false, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据对象全局唯一标识集合获取实体集合
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<TPrimaryKey> ids,
            bool includeDetails = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取所有实体的集合
        /// </summary>
        /// <param name="includeDetails">包含详情信息（一对一、一对多关系）</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(
            bool includeDetails = false,
            CancellationToken cancellationToken = default);

    }




}
