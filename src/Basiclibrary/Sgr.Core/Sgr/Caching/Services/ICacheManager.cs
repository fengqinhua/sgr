/**************************************************************
 * 
 * 唯一标识：397c8b4e-60a5-4564-9f4d-0d0558c7c96a
 * 命名空间：Sgr.Caching.Services
 * 创建时间：2023/8/23 7:21:17
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
    public interface ICacheManager : IDisposable
    {
        //void Set(string key, object obj, CacheEntryOptions? cacheEntryOptions);
        //Task SetAsync(string key, object obj, CacheEntryOptions? cacheEntryOptions);

        Task<TData> GetAsync<TData>(string key, Func<Task<TData>> acquire, CacheEntryOptions? cacheEntryOptions);
        Task<TData> GetAsync<TData>(string key, Func<TData> acquire, CacheEntryOptions? cacheEntryOptions);
        TData Get<TData>(string key, Func<TData> acquire, CacheEntryOptions? cacheEntryOptions);

        Task RemoveAsync(string key);
        void Remove(string key);

        Task RemoveByPrefixAsync(string prefix);
        void RemoveByPrefix(string prefix);

        Task ClearAsync();
        void Clear();

        CacheEntryOptions CreateCacheEntryOptions();
    }
}
