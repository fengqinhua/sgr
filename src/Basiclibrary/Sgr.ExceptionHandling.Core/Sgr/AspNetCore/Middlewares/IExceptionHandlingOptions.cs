/**************************************************************
 * 
 * 唯一标识：0d3a8764-1564-4277-8ede-127f5ce87571
 * 命名空间：Sgr.AspNetCore.Middlewares
 * 创建时间：2023/8/18 21:28:44
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.AspNetCore.Middlewares
{
    public interface IExceptionHandlingOptions
    {
        /// <summary>
        /// 是否包含详细的错误信息
        /// </summary>
        bool IncludeFullDetails { get; set; }
    }
}
