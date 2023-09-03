/**************************************************************
 * 
 * 唯一标识：658301b6-3f04-428c-a641-3a8d78d81659
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 17:52:59
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Caching.Services;
using System;
using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    public class RemoveCacheBackGroundTask : IBackGroundTask<RemoveCacheArgs>
    {
        private readonly ICacheManager _cacheManager;

        public RemoveCacheBackGroundTask(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task ExecuteAsync(RemoveCacheArgs data)
        {
            if (data == null)
                return;

            if (!string.IsNullOrEmpty(data.Key))
                await _cacheManager.RemoveAsync(data.Key!);

            if (!string.IsNullOrEmpty(data.Prefix))
                await _cacheManager.RemoveByPrefixAsync(data.Prefix!);
        }
    }
}
