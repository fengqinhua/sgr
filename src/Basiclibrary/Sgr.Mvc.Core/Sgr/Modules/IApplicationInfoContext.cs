/**************************************************************
 * 
 * 唯一标识：bb2920be-283e-4bf3-9b02-db496b90430e
 * 命名空间：Sgr.Modules
 * 创建时间：2023/7/30 9:53:38
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


namespace Sgr.Modules
{
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public interface IApplicationInfoContext
    {
        /// <summary>
        /// 应用程序信息
        /// </summary>
        ApplicationInfo Application { get; }
    }
}
