/**************************************************************
 * 
 * 唯一标识：85e664da-1819-4cb4-be2b-4cb9845c520d
 * 命名空间：Sgr.Middlewares
 * 创建时间：2023/8/8 15:22:52
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.AspNetCore.Middlewares.PoweredBy
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPoweredByMiddlewareOptions
    {
        /// <summary>
        /// 
        /// </summary>
        bool Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string HeaderName { get; }
        /// <summary>
        /// 
        /// </summary>
        string HeaderValue { get; set; }
    }
}
