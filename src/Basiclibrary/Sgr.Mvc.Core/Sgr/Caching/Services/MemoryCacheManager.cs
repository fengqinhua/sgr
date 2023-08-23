/**************************************************************
 * 
 * 唯一标识：0068360b-b7ef-4980-802c-0ac9a4973466
 * 命名空间：Sgr.Caching.Services
 * 创建时间：2023/8/23 7:21:33
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Sgr.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sgr.Caching.Services
{
    public class MemoryCacheManager : ICacheManager
    {
        private static readonly object _mutex = new();

        private bool _disposed;
        private CancellationTokenSource _clearToken = new();

        private readonly List<string> _keysList = new();

        private readonly CacheOptions _cacheOptions;
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager(CacheOptions cacheOptions, IMemoryCache memoryCache)
        {
            _cacheOptions = cacheOptions;
            _memoryCache = memoryCache;
        }

        #region ICacheManager

        public Task SetAsync(string key, object obj, CacheEntryOptions? cacheEntryOptions)
        {
            Set(key, obj, cacheEntryOptions);
            return Task.CompletedTask;
        }

        public void Set(string key, object obj, CacheEntryOptions? cacheEntryOptions)
        {
            if (string.IsNullOrEmpty(key))
                return;

            if (obj == null)
                return;

            _memoryCache.Set(key, obj, prepareMemoryCacheEntryOptions(cacheEntryOptions));

            OnAddKey(key);
        }

        public async Task<TData> GetAsync<TData>(string key, Func<Task<TData>> acquire, CacheEntryOptions? cacheEntryOptions)
        {
            if (string.IsNullOrEmpty(key))
                throw new BusinessException("ICacheManager.GetAsync Parameter Key Is Null Or Empty!");

            bool isNew = false;

            var result = await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.SetOptions(prepareMemoryCacheEntryOptions(cacheEntryOptions));
                isNew = true; 
                return await acquire();
            });

            //do not cache null value
            if (result == null)
                await RemoveAsync(key);
            else
            {
                if(isNew)
                    OnAddKey(key);
            }

            return result!;
        }

        public async Task<TData> GetAsync<TData>(string key, Func<TData> acquire, CacheEntryOptions? cacheEntryOptions)
        {
            if (string.IsNullOrEmpty(key))
                throw new BusinessException("ICacheManager.GetAsync Parameter Key Is Null Or Empty!");

            bool isNew = false;

            var result = _memoryCache.GetOrCreate(key, entry =>
            {
                entry.SetOptions(prepareMemoryCacheEntryOptions(cacheEntryOptions));
                isNew = true;
                return acquire();
            });

            //do not cache null value
            if (result == null)
                await RemoveAsync(key);
            else
            {
                if (isNew)
                    OnAddKey(key);
            }

            return result!;
        }

        public TData Get<TData>(string key, Func<TData> acquire, CacheEntryOptions? cacheEntryOptions)
        {
            if (string.IsNullOrEmpty(key))
                return acquire();

            bool isNew = false;

            var result = _memoryCache.GetOrCreate(key, entry =>
            {
                entry.SetOptions(prepareMemoryCacheEntryOptions(cacheEntryOptions));
                isNew = true;
                return acquire();
            });

            //do not cache null value
            if (result == null)
                 Remove(key);
            else
            {
                if (isNew)
                    OnAddKey(key);
            }
            return result!;
        }

        public Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
            OnRemoveKey(key);
        }

        public Task RemoveByPrefixAsync(string prefix)
        {
            RemoveByPrefix(prefix);

            return Task.CompletedTask;
        }

        public void RemoveByPrefix(string prefix)
        {
            lock (_mutex)
            {
                foreach (var key in _keysList
                    .Where(key => key.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                    .ToList())
                {
                    _memoryCache.Remove(key);
                    _keysList.Remove(key);
                }
            }
        }

        public Task ClearAsync()
        {
            Clear();

            return Task.CompletedTask;
        }

        public void Clear()
        {
            _clearToken.Cancel();
            _clearToken.Dispose();

            _clearToken = new CancellationTokenSource();

            _keysList.Clear();
        }

        public CacheEntryOptions CreateCacheEntryOptions()
        {
            var cacheEntryOptions = new CacheEntryOptions();
            if (_cacheOptions.DefaultAbsoluteExpirationSecond.HasValue)
                cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheOptions.DefaultAbsoluteExpirationSecond.Value);

            if (_cacheOptions.DefaultSlidingExpirationSecond.HasValue)
                cacheEntryOptions.SlidingExpiration = TimeSpan.FromSeconds(_cacheOptions.DefaultSlidingExpirationSecond.Value);

            return cacheEntryOptions;
        }

        #region Dispose

        /// <summary>
        /// Dispose cache manager
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _memoryCache.Dispose();

            _disposed = true;
        }

        #endregion


        #endregion

        protected virtual MemoryCacheEntryOptions prepareMemoryCacheEntryOptions(CacheEntryOptions? cacheEntryOptions)
        {
            //设置过期时间
            var options = new MemoryCacheEntryOptions();

            if (cacheEntryOptions != null)
            {
                options.SlidingExpiration = cacheEntryOptions!.SlidingExpiration;
                options.AbsoluteExpirationRelativeToNow = cacheEntryOptions!.AbsoluteExpirationRelativeToNow;
                options.AbsoluteExpiration = cacheEntryOptions!.AbsoluteExpiration;
            }

            //设置清理缓存所需的令牌
            options.AddExpirationToken(new CancellationChangeToken(_clearToken.Token));

            return options;
        }

        protected virtual void OnAddKey(string key)
        {
            lock (_mutex)
            {
                if (!_keysList.Contains(key))
                    _keysList.Add(key);
            }
        }

        protected virtual void OnRemoveKey(string key)
        {
            lock (_mutex)
            {
                if (!_keysList.Contains(key))
                    _keysList.Remove(key);
            }
        }

    }
}
