/**************************************************************
 * 
 * 唯一标识：27a4f43d-3846-4bf5-b902-e83a5de87d10
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 16:31:14
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System.Reflection;
using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    public class DefaultBackGroundTaskExecutor : IBackGroundTaskExecutor
    {
        public async Task ExecuteAsync(BackGroundTaskContext context)
        {
            var taskClassObj = context.ServiceProvider.GetService(context.TaskClassType) ?? throw new BackGroundTaskException($"TaskClassType( {context.TaskClassType.FullName} )未注入至 Dependency Injection中!");

            MethodInfo methodInfo = context.TaskClassType.GetMethod("ExecuteAsync") ?? throw new BackGroundTaskException($"TaskClassType ({context.TaskClassType.FullName}) 未定义 ExecuteAsync 函数!");

            if (context.TaskData != null)
                await (Task)methodInfo.Invoke(taskClassObj, new object[] { context.TaskData! });
            else
                await (Task)methodInfo.Invoke(taskClassObj, new object[] { });
        }
    }
}
