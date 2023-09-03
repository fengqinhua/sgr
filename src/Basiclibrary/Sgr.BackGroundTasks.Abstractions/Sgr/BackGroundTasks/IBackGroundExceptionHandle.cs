/**************************************************************
 * 
 * 唯一标识：fd217a88-c0a9-46e0-86e3-47be9134d34e
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 16:54:11
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System.Threading.Tasks;

namespace Sgr.BackGroundTasks
{
    public interface IBackGroundExceptionHandle
    {
        /// <summary>
        /// 当任务执行失败次数超过设定的上限时需要做的处理
        /// </summary>
        /// <param name="backGroundTaskContext"></param>
        /// <returns></returns>
        Task OnTaskNeverSucceedAsync(BackGroundTaskContext backGroundTaskContext);

        /// <summary>
        /// 当某一次任务执行失败所需执行的处理
        /// </summary>
        /// <param name="backGroundTaskContext"></param>
        /// <param name="currentRefireCount"></param>
        /// <returns></returns>
        Task OnTaskFailAsync(BackGroundTaskContext backGroundTaskContext, int currentRefireCount);
    }
}
