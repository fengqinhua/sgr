/**************************************************************
 * 
 * 唯一标识：8392fe20-1174-491c-9d6f-1d2821952dfb
 * 命名空间：Sgr.Caching.Services
 * 创建时间：2023/9/6 11:19:13
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
using System.Threading;

namespace Sgr.DistributedLock
{
    public interface IDistributedLock
    {
        IDisposable? TryGet(string key, TimeSpan timeout = default);
        Task<IAsyncDisposable?> TryGetAsync(string key, TimeSpan timeout = default, CancellationToken cancellationToken = default);
    }
}
