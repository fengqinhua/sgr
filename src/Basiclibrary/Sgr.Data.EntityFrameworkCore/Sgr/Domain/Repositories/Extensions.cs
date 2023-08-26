/**************************************************************
 * 
 * 唯一标识：df81b85c-a2f4-40ca-a896-9e3560e548d2
 * 命名空间：Sgr.Domain.Repositories
 * 创建时间：2023/8/26 11:15:53
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
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.Domain.Repositories
{
    public static class Extensions
    {
        #region IMustHaveOrg

        /// <summary>
        /// 根据OrgId获取数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <typeparam name="TOrgKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="orgId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TEntity>> GetByOrgIdAsync<TEntity, TPrimaryKey, TOrgKey>(this IBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> repository,
            TOrgKey orgId,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TPrimaryKey>, IAggregateRoot, IMustHaveOrg<TOrgKey>
        {
            if (repository is EfCoreRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> efcore_repository)
                return await efcore_repository.GetDbContextToExtensions().Set<TEntity>().Where(f => f.OrgId!.Equals(orgId)).ToArrayAsync(cancellationToken);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据OrgId删除所有数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <typeparam name="TOrgKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="orgId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task DeleteByOrgIdAsync<TEntity, TPrimaryKey, TOrgKey>(this IBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> repository,
            TOrgKey orgId,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TPrimaryKey>, IAggregateRoot, IMustHaveOrg<TOrgKey>
        {
            var list = await repository.GetByOrgIdAsync(orgId, cancellationToken);
            foreach (TEntity item in list)
            {
                await repository.DeleteAsync(item, cancellationToken);
            }
        }


        #endregion

        #region IHaveCode

        /// <summary>
        /// 检查Code是否唯一
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static async Task<bool> CodeIsUniqueAsync<TEntity, TPrimaryKey>(this IBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> repository,
            string code, 
            TPrimaryKey id, 
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TPrimaryKey>, IAggregateRoot, IHaveCode
        {
            if (repository is EfCoreRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> efcore_repository)
                return await efcore_repository.GetDbContextToExtensions().Set<TEntity>().AnyAsync(f => f.Code == code && f.Id!.Equals(id), cancellationToken);

            throw new NotImplementedException();
        }

        #endregion

        #region ISoftDelete

        /// <summary>
        /// 按照软删除的状态，获取所有数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="isDeleted"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<TEntity>> GetByDeletedStatuAsync<TEntity, TPrimaryKey>(this IBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> repository,
            bool isDeleted,
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntity<TPrimaryKey>, IAggregateRoot, ISoftDelete
        {
            if (repository is EfCoreRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> efcore_repository)
                return await efcore_repository.GetDbContextToExtensions().Set<TEntity>().Where(f => f.IsDeleted == isDeleted).ToArrayAsync(cancellationToken);

            throw new NotImplementedException();
        }

        #endregion


    }
}
