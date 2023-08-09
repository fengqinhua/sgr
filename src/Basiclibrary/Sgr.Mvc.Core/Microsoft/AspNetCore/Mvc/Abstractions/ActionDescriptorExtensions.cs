/**************************************************************
 * 
 * 唯一标识：a8491261-0824-4a20-bbb7-45706a9e2b25
 * 命名空间：Microsoft.AspNetCore.Mvc.Abstractions
 * 创建时间：2023/8/9 8:05:06
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Microsoft.AspNetCore.Mvc.Abstractions
{
    /// <summary>
    /// 扩展函数
    /// </summary>
    public static class ActionDescriptorExtensions
    {
        /// <summary>
        /// IsControllerAction
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static bool IsControllerAction(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor is ControllerActionDescriptor;
        }
        /// <summary>
        /// IsPageAction
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static bool IsPageAction(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor is PageActionDescriptor;
        }
    }
}
