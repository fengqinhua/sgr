/**************************************************************
 * 
 * 唯一标识：0ecfaa53-f3f7-4cc5-af5c-42ad3dbb7c35
 * 命名空间：Sgr.Domain.Uow
 * 创建时间：2023/7/27 11:32:42
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;

namespace Sgr.Domain.Uow
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ///// <summary>
        ///// 保存
        ///// </summary>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        //Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 是否存在活动的事务
        /// </summary>
        bool HasActiveTransaction { get; }
        /// <summary>
        /// 添加事务提交成功后的处理事件
        /// </summary>
        /// <param name="handler"></param>
        void AddTransactionCompletedHandler(Func<Task> handler);
        /// <summary>
        /// 执行Save操作
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }

}
