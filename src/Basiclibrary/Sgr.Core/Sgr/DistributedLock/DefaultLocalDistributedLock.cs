/**************************************************************
 * 
 * 唯一标识：cf9cfe5d-d76d-4f56-aec0-56c61213aadc
 * 命名空间：Sgr.Caching.Services
 * 创建时间：2023/9/6 15:51:37
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.DistributedLock
{
    public class DefaultLocalDistributedLock : IDistributedLock
    {
        private readonly ConcurrentDictionary<string, SemaphoreSlim> _localObjects = new();

        public IDisposable? TryGet(string key, TimeSpan timeout = default)
        {
            SemaphoreSlim semaphore = GetSemaphoreSlim(key);
            if (!semaphore.Wait(timeout))
            {
                return null;
            }

            return new LocalDistributedLockDisposeAction(semaphore);
        }

        public async Task<IAsyncDisposable?> TryGetAsync(string key, TimeSpan timeout = default, CancellationToken cancellationToken = default)
        {
            SemaphoreSlim semaphore = GetSemaphoreSlim(key);
            if (!await semaphore.WaitAsync(timeout, cancellationToken))
            {
                return null;
            }

            return new LocalDistributedLockDisposeAction(semaphore);
        }


        private SemaphoreSlim GetSemaphoreSlim(string key)
        {
            Check.StringNotNullOrEmpty(key, nameof(key));
            return _localObjects.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        }
    }
}
