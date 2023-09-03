/**************************************************************
 * 
 * 唯一标识：1abca37d-0099-4866-91eb-a1f3154e3866
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/3 17:18:55
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.DependencyInjection;
using Sgr.Admin.WebHost.Tests.Sgr;
using Sgr.BackGroundTasks.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    [TestClass]
    public class RecurringTaskManagerTests : SgrAdminWebBaseTests
    {
        [TestMethod]
        public void RecurringTaskRun()
        {
            Thread.Sleep(1000);

            //获取当前时间 dateTimeOffset1 ，然后等6秒后再获取当前时间 dateTimeOffset2 ，比较两个时间的差值是否符合预期
            DateTimeOffset dateTimeOffset1 = TimerBackGroundTask.DateTimeValue;

            Thread.Sleep(6000);

            DateTimeOffset dateTimeOffset2 = TimerBackGroundTask.DateTimeValue;

            int timeSpanSeconds = (dateTimeOffset2 - dateTimeOffset1).Seconds;

            Assert.IsTrue(timeSpanSeconds <= 6 && timeSpanSeconds >= 4);
        }


        [TestMethod]
        public async Task RecurringTaskStartAndStop()
        {
            IRecurringTaskManager recurringTaskManager = base.TestServer.Services.GetRequiredService<IRecurringTaskManager>();
            //停止周期性任务
            await recurringTaskManager.StopAsync(ModuleStartup.TimerBackGroundTaskId);

            //检查周期性任务停止后 DateTimeValue 是否已不再更新
            Thread.Sleep(2000);
            DateTimeOffset dateTimeOffset1 = TimerBackGroundTask.DateTimeValue;
            Thread.Sleep(2000);
            DateTimeOffset dateTimeOffset2 = TimerBackGroundTask.DateTimeValue;

            Assert.AreEqual(dateTimeOffset1, dateTimeOffset2);

            //重启周期性任务
            await recurringTaskManager.ReStartAsync(ModuleStartup.TimerBackGroundTaskId);

            //检查周期性任务运行状况
            RecurringTaskRun();

        }
    }
}
