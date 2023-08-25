/**************************************************************
 * 
 * 唯一标识：528aa5ba-3976-4801-87a2-b990a295506f
 * 命名空间：Sgr.AspNetCore.Middlewares
 * 创建时间：2023/8/18 21:29:42
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.AspNetCore.Middlewares.ExceptionHandling
{
    public class ExceptionHandlingOptions : IExceptionHandlingOptions
    {
        /// <summary>
        /// 是否包含详细的错误信息
        /// </summary>
        public bool IncludeFullDetails { get; set; } = false;
    }
}
