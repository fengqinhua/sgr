/**************************************************************
 * 
 * 唯一标识：5f0bf2a7-03ff-49df-9d1a-6b7a683c7bd0
 * 命名空间：Sgr.Caching.Services
 * 创建时间：2023/8/23 13:29:52
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Caching.Services
{
    public class NoCacheManager : ICacheManager
    {
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public CacheEntryOptions CreateCacheEntryOptions()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TData Get<TData>(string key, Func<TData> acquire, CacheEntryOptions? cacheEntryOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<TData> GetAsync<TData>(string key, Func<Task<TData>> acquire, CacheEntryOptions? cacheEntryOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<TData> GetAsync<TData>(string key, Func<TData> acquire, CacheEntryOptions? cacheEntryOptions = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveByPrefix(string prefix)
        {
            throw new NotImplementedException();
        }

        public Task RemoveByPrefixAsync(string prefix)
        {
            throw new NotImplementedException();
        }
    }
}
