/**************************************************************
 * 
 * 唯一标识：8368563c-dab0-4ea3-b87a-9f75a09d5888
 * 命名空间：Sgr.Domain.Repositories
 * 创建时间：2023/7/27 10:41:38
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sgr.Domain.Entities.Auditing;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Sgr.Domain.Uow;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Sgr.Domain.Repositories
{
    /// <summary>
    /// 基于EF CORE 实现的 IBaseRepository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class EfCoreRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> : IBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, IAggregateRoot
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        public abstract IUnitOfWork UnitOfWork { get; }

        #region 待实现或扩展的方法

        /// <summary>
        /// 获取ef core DbContext
        /// </summary>
        protected abstract DbContext GetDbContext();

        /// <summary>
        /// 获取实体审计者
        /// </summary>
        /// <returns></returns>
        protected abstract IAuditedOperator? GetAuditedOperator();

        /// <summary>
        /// 检查并设置主键Id
        /// </summary>
        /// <param name="entity"></param>
        protected abstract void CheckAndSetId(TEntity entity);

        /// <summary>
        /// 更新实体创建相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        protected virtual void UpdateCreationAudited(TEntity targetObject)
        {
            this.GetAuditedOperator()?.UpdateCreationAudited(targetObject);
        }

        /// <summary>
        /// 更新实体修改相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        protected virtual void UpdateModifyAudited(TEntity targetObject)
        {
            this.GetAuditedOperator()?.UpdateModifyAudited(targetObject);
        }

        /// <summary>
        /// 更新实体删除相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        protected virtual void UpdateDeleteAudited(TEntity targetObject)
        {
            this.GetAuditedOperator()?.UpdateDeleteAudited(targetObject);
        }

        /// <summary>
        /// 检查实体是否设置了软删除
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckEntityIsSoftDelete()
        {
            return typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity));
        }

        /// <summary>
        /// 获取IQueryable
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetQueryable()
        { 
            return GetDbContext().Set<TEntity>();
        }

        /// <summary>
        /// 获取IQueryable
        /// </summary>
        /// <param name="propertiesToInclude"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetQueryable(string[] propertiesToInclude)
        {
            IQueryable<TEntity> queryable = GetQueryable();
            if (propertiesToInclude != null)
            {
                foreach (var property in propertiesToInclude)
                {
                    queryable = queryable.Include(property);
                }
            }

            return queryable;
        }
        

        #endregion

        #region IBaseRepositoryOfTEntityAndTPrimaryKey

        #region INSERT

        /// <summary>
        /// 创建一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Check.NotNull(entity, nameof(entity));

            this.CheckAndSetId(entity);
            this.UpdateCreationAudited(entity);

            var dbContext = GetDbContext();

            return (await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken)).Entity;
        }

        /// <summary>
        /// 创建多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                await InsertAsync(entity, cancellationToken);
            }
        }

        #endregion

        #region UPDATE
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            Check.NotNull(entity, nameof(entity));

            this.UpdateModifyAudited(entity);

            var dbContext = GetDbContext();
            dbContext.Entry(entity).State = EntityState.Modified;
            return entity;

            //dbContext.Attach(entity);
            //return dbContext.Update(entity).Entity;
        }
        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                await UpdateAsync(entity, cancellationToken);
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Check.NotNull(entity, nameof(entity));

            this.UpdateDeleteAudited(entity);

            if (CheckEntityIsSoftDelete())
                await UpdateAsync(entity, cancellationToken);
            else
            {
                var dbContext = GetDbContext();
                dbContext.Set<TEntity>().Remove(entity);
            }
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity != null)
                await DeleteAsync(entity, cancellationToken);
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task BulkDeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                await DeleteAsync(entity, cancellationToken);
            }
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task BulkDeleteAsync(IEnumerable<TPrimaryKey> ids, CancellationToken cancellationToken = default)
        {
            var entities = await GetByIdsAsync(ids, cancellationToken);
            await BulkDeleteAsync(entities, cancellationToken);
        }

        #endregion

        #region IReadOnlyBaseRepositoryOfTEntityAndTPrimaryKey

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<long> CountAsync(CancellationToken cancellationToken = default)
        {
            var dbContext = GetDbContext();
            return await dbContext.Set<TEntity>().LongCountAsync(cancellationToken);
        }

        /// <summary>
        /// 根据对象全局唯一标识获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity?> GetAsync(TPrimaryKey id,
            CancellationToken cancellationToken = default)
        {
            var dbContext = GetDbContext();
            return await dbContext!.Set<TEntity>().FindAsync(keyValues: new object?[] { id }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 根据对象全局唯一标识集合获取实体集合
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<TPrimaryKey> ids,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .Where(f => ids.Contains(f.Id))
                .OrderBy(f => f.Id)
                .ToArrayAsync(cancellationToken);
        }

        /// <summary>
        /// 获取所有实体的集合
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .OrderBy(f => f.Id)
                .ToArrayAsync(cancellationToken);
        }

        /// <summary>
        /// 根据对象全局唯一标识获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity?> GetAsync(TPrimaryKey id,
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(propertiesToInclude).FirstOrDefaultAsync(f=>f.Id!.Equals(id), cancellationToken);
        }

        /// <summary>
        /// 根据对象全局唯一标识集合获取实体集合
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<TPrimaryKey> ids,
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default)
        { 
            return await GetQueryable(propertiesToInclude)
                 .Where(f => ids.Contains(f.Id))
                 .OrderBy(f => f.Id)
                 .ToArrayAsync(cancellationToken);
        }

        /// <summary>
        /// 获取所有实体的集合
        /// </summary>
        /// <param name="propertiesToInclude"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default)
        {
            return await GetQueryable(propertiesToInclude)
                .OrderBy(f => f.Id)
                .ToArrayAsync(cancellationToken);

        }

        #region  related-data/explicit

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task CollectionAsync<TProperty>(TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> propertyExpression,
            CancellationToken cancellationToken = default) where TProperty : class
        {
            var dbContext = GetDbContext();
            EntityEntry<TEntity> entityEntry = GetExplicitEntityEntry(entity, dbContext);
            await entityEntry.Collection(propertyExpression).LoadAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task CollectionAsync(TEntity entity,
            string propertyName,
            CancellationToken cancellationToken = default)
        {
            var dbContext = GetDbContext(); 
            var navigation = dbContext.Entry(entity).Metadata.FindNavigation(propertyName);

            if (navigation != null)
            {
                EntityEntry<TEntity> entityEntry = GetExplicitEntityEntry(entity, dbContext);
                await entityEntry.Collection(navigation).LoadAsync(cancellationToken);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task ReferenceAsync<TProperty>(TEntity entity,
            Expression<Func<TEntity, TProperty?>> propertyExpression,
            CancellationToken cancellationToken = default) where TProperty : class
        {
            var dbContext = GetDbContext();
            EntityEntry<TEntity> entityEntry = GetExplicitEntityEntry(entity, dbContext);
            await entityEntry.Reference(propertyExpression).LoadAsync(cancellationToken);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task ReferenceAsync(TEntity entity,
            string propertyName,
            CancellationToken cancellationToken = default)
        {
            var dbContext = GetDbContext();
            var navigation = dbContext.Entry(entity).Metadata.FindNavigation(propertyName);
            if (navigation != null)
            {
                EntityEntry<TEntity> entityEntry = GetExplicitEntityEntry(entity, dbContext);
                await entityEntry.Reference(navigation).LoadAsync(cancellationToken);
            }

        }


        protected virtual EntityEntry<TEntity> GetExplicitEntityEntry(TEntity entity, DbContext dbContext)
        {
            var entityEntry = dbContext.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
                entityEntry.State = EntityState.Unchanged;
            return entityEntry;
        }


        #endregion


        #endregion

        #endregion

    }
}
