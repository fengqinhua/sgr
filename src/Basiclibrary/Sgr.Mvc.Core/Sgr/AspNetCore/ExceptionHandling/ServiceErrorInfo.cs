/**************************************************************
 * 
 * 唯一标识：0f759dc9-5176-4736-bbe6-ad2e61ba1545
 * 命名空间：Sgr.AspNetCore
 * 创建时间：2023/8/10 21:25:20
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System.Collections;

namespace Sgr.AspNetCore.ExceptionHandling
{
    /// <summary>
    /// 服务端异常信息
    /// </summary>
    public class ServiceErrorInfo
    {
        /// <summary>
        /// 异常消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 异常编号
        /// </summary>
        public string? No { get; set; }
        /// <summary>
        /// 异常详情
        /// </summary>
        public string? Details { get; set; }
        /// <summary>
        /// 验证错误
        /// </summary>
        public ServiceValidationErrorInfo[]? ValidationErrors { get; set; }
        /// <summary>
        /// 异常数据
        /// </summary>
        public IDictionary? Data { get; set; }
    }
}
