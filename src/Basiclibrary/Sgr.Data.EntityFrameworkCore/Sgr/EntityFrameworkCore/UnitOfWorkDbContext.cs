/**************************************************************
 * 
 * 唯一标识：1903f1c2-d8c3-43e2-9a74-802cd0bfddbe
 * 命名空间：Sgr.EntityFrameworkCore
 * 创建时间：2023/8/4 9:19:41
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sgr.Domain.Uow;
using Sgr.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.EntityFrameworkCore
{
    /// <summary>
    /// 支持IUnitOfWork的DbContext
    /// </summary>
    public abstract class UnitOfWorkDbContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// 中介者，处理领域事件
        /// </summary>
        protected readonly IMediator _mediator;

        /// <summary>
        /// 数据库事务
        /// </summary>
        protected IDbContextTransaction? _currentTransaction;

        /// <summary>
        /// 数据库事务提交成功后的处理事件
        /// </summary>
        protected IList<Func<Task>> _completedHandlers;
        /// <summary>
        /// 事务是否已提交
        /// </summary>
        protected bool _isCompleted;

        /// <summary>
        /// 支持IUnitOfWork的DbContext
        /// </summary>
        /// <param name="options"></param>
        /// <param name="mediator"></param>
        public UnitOfWorkDbContext(DbContextOptions options, IMediator mediator)
            : base(options)
        {
            Check.NotNull(mediator, nameof(mediator));

            this._mediator = mediator;
            this._completedHandlers = new List<Func<Task>>();
            this._isCompleted = false;
#if DEBUG
            System.Diagnostics.Debug.WriteLine(this.GetType() + "::ctor ->" + this.GetHashCode());
#endif
        }

        /// <summary>
        /// 是否有活动的事务
        /// </summary>
        public virtual bool HasActiveTransaction => this._currentTransaction != null && !this._isCompleted;
        /// <summary>
        /// 添加事务提交成功后的处理事件
        /// </summary>
        /// <param name="handler"></param>
        public virtual void AddTransactionCompletedHandler(Func<Task> handler)
        {
            _completedHandlers.Add(handler);
        }

        /// <summary>
        /// 获取当前活动的事务
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;

        #region IUnitOfWork


        /// <summary>
        /// 执行Save操作
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            //this.ChangeTracker.DetectChanges();

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        #endregion

        #region 事务处理





        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction?> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            this._currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            this._isCompleted = false;

            return _currentTransaction;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new InvalidOperationException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();

                this._isCompleted = true;

                //数据储存了，事务提交了，则说明工作单元已经完成了，遍历完成事件集合，依次调用这些方法。
                await OnCompletedAsync();
            }
            catch
            {
                if (!this._isCompleted)
                    RollbackTransaction();

                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected virtual async Task OnCompletedAsync()
        {
            foreach (var handler in _completedHandlers)
            {
                await handler.Invoke();
            }
        }

        #endregion 

    }
}
