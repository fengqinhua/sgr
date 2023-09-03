/**************************************************************
 * 
 * 唯一标识：366c6d0f-eec9-4174-b8e8-be3a64fc781b
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/3 11:17:45
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Sgr.Admin.WebHost.Tests.Sgr;
using Sgr.Caching.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    [TestClass]
    public class BackGroundTaskManagerTests : SgrAdminWebBaseTests
    {
        [TestMethod]
        public async Task EnqueueBackGroundTask()
        {
            IBackGroundTaskManager backGroundTaskManager = base.TestServer.Services.GetRequiredService<IBackGroundTaskManager>();

            CounterBackGroundTask.Index = 0;

            //计数器+1 立即执行
            await backGroundTaskManager.EnqueueAsync<CounterBackGroundTask>();
            //稍等一下，等待任务完成
            Thread.Sleep(1000);
            Assert.AreEqual(CounterBackGroundTask.Index, 1);

            //计数器+1，延迟2秒执行
            await backGroundTaskManager.EnqueueAsync<CounterBackGroundTask>(delay :TimeSpan.FromMilliseconds(2000));
            Thread.Sleep(1000);
            Assert.AreEqual(CounterBackGroundTask.Index, 1);

            Thread.Sleep(2000);
            Assert.AreEqual(CounterBackGroundTask.Index, 2);
        }

        [TestMethod]
        public async Task EnqueueBackGroundTaskWithData()
        {
            ICacheManager cacheManager = base.TestServer.Services.GetRequiredService<ICacheManager>();
            IBackGroundTaskManager backGroundTaskManager = base.TestServer.Services.GetRequiredService<IBackGroundTaskManager>();

            //先设置缓存
            string key = "cache-key";
            string prefix = "cache";
            string value1 = "abc";
            string value2 = "def";
            cacheManager.Get(key, () =>
            {
                return value1;
            });

            //添加后台任务，清理缓存
            await backGroundTaskManager.EnqueueAsync<RemoveCacheBackGroundTask, RemoveCacheArgs>(
                new RemoveCacheArgs() { Prefix = prefix }
            );
            //稍等一下，等待任务完成
            Thread.Sleep(100);

            //重新获取缓存
            string val = cacheManager.Get(key, () =>
            {
                return value2;
            });

            //检查缓存值是否为新值
            Assert.AreEqual(val, value2);
        }
    }
}
