/**************************************************************
 * 
 * 唯一标识：a7134f0e-24ca-44aa-8532-db174f9f42c5
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 11:33:51
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sgr.BackGroundTasks
{

    /// <summary>
    /// 实现该接口来定义一个任务（无入参）
    /// </summary>
    public interface IBackGroundTask
    {
        Task ExecuteAsync();
    }

    /// <summary>
    /// 实现该接口来定义一个任务（含有入参）
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface IBackGroundTask<TData> where TData : class
    {
        /// <summary>
        /// 执行后台任务
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task ExecuteAsync(TData data);
    }


}
