/**************************************************************
 * 
 * 唯一标识：0fffab6d-3a79-4913-8a22-0bc9565a73b9
 * 命名空间：Sgr.Caching.Services
 * 创建时间：2023/9/6 15:38:26
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.DistributedLock
{
    internal sealed class LocalDistributedLockDisposeAction : IDisposable, IAsyncDisposable
    {
        private readonly SemaphoreSlim _semaphore;

        public LocalDistributedLockDisposeAction(SemaphoreSlim semaphore)
        {
            _semaphore = semaphore;
        }

        public ValueTask DisposeAsync()
        {
            _semaphore.Release();
            return default;
        }

        public void Dispose()
        {
            _semaphore.Release();
        }
    }
}
