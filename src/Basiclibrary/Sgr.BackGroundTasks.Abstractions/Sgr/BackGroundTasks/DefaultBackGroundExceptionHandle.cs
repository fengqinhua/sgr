/**************************************************************
 * 
 * 唯一标识：b879c626-3b09-4d32-818f-0ba57887fc6a
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 16:59:53
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    public class DefaultBackGroundExceptionHandle : IBackGroundExceptionHandle
    {
        /// <summary>
        /// 当任务执行失败次数超过设定的上限时需要做的处理
        /// </summary>
        /// <param name="backGroundTaskContext"></param>
        /// <returns></returns>
        public Task OnTaskNeverSucceedAsync(BackGroundTaskContext backGroundTaskContext)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 当某一次任务执行失败所需执行的处理
        /// </summary>
        /// <param name="backGroundTaskContext"></param>
        /// <param name="currentRefireCount"></param>
        /// <returns></returns>
        public Task OnTaskFailAsync(BackGroundTaskContext backGroundTaskContext, int currentRefireCount)
        {
            return Task.CompletedTask;
        }
    }
}
