/**************************************************************
 * 
 * 唯一标识：8e7377fd-fdfd-44d3-aae2-c5d16969de2b
 * 命名空间：Microsoft.AspNetCore.Mvc
 * 创建时间：2023/8/25 16:48:56
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.ExceptionHandling;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerBaseExtensions
    {
        /// <summary>
        /// 自定义400结果(BadRequest)
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static BadRequestObjectResult CustomBadRequest(this ControllerBase controller, string message = "错误的请求")
        {
            return controller.BadRequest(ServiceErrorResponse.CreateNew(message));
        }

        /// <summary>
        /// 自定义401结果(Unauthorized)
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static UnauthorizedObjectResult CustomUnauthorized(this ControllerBase controller,string message = "无访问权限")
        {
            return controller.Unauthorized(ServiceErrorResponse.CreateNew(message));
        }

        /// <summary>
        /// 自定义404结果(NotFound)
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static NotFoundObjectResult CustomNotFound(this ControllerBase controller, string message = "您所请求的资源无法找到")
        {
            return controller.NotFound(ServiceErrorResponse.CreateNew(message));
        }

    }
}
