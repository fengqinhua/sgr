/**************************************************************
 * 
 * 唯一标识：30e88d62-645d-44ff-b88a-4730f063669e
 * 命名空间：Sgr.AspNetCore
 * 创建时间：2023/8/10 21:27:09
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.AspNetCore.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceErrorResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceErrorInfo Serviceerror { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public ServiceErrorResponse(ServiceErrorInfo error)
        {
            Serviceerror = error;
        }

        public static ServiceErrorResponse CreateNew(string message)
        {
            return new ServiceErrorResponse(new ServiceErrorInfo()
            {
                Message = message
            });
        }
    }
}
