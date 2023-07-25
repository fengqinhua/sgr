/**************************************************************
 * 
 * 唯一标识：0e2f513f-93f7-4b89-ab3c-bf2b1c35c158
 * 命名空间：Sgr.Exceptions
 * 创建时间：2023/7/20 21:55:15
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Exceptions
{
    /// <summary>
    /// 用户友好的异常信息
    /// </summary>
    [Serializable]
    public class UserFriendlyException : BusinessException
    {
        /// <summary>
        /// 用户友好的异常信息
        /// </summary>
        /// <param name="message"></param>
        public UserFriendlyException(string message)
            : base(message)
        {

        }
    }
}
