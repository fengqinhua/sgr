/**************************************************************
 * 
 * 唯一标识：02934b0e-5f7a-47f5-b9e7-ef7231705373
 * 命名空间：Sgr.Exceptions
 * 创建时间：2023/7/20 21:53:19
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sgr.Exceptions
{
    /// <summary>
    /// 应用内部异常信息
    /// </summary>
    [Serializable]
    public class BusinessException : Exception
    {
        /// <summary>
        /// 应用内部异常信息
        /// </summary>
        /// <param name="message">错误消息文本内容.</param>
        public BusinessException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 应用内部异常信息
        /// </summary>
		/// <param name="messageFormat">错误消息文本内容模板.</param>
		/// <param name="args">错误消息文本内容参数.</param>
        public BusinessException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        /// <summary>
        /// 应用内部异常信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected BusinessException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 应用内部异常信息
        /// </summary>
        /// <param name="message">错误消息文本内容.</param>
        /// <param name="innerException">引发当前异常的异常信息.</param>
        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 异常数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public BusinessException WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }

        ///// <summary>
        ///// 异常编号
        ///// </summary>
        //public string ErrorNo { get; set; } = "500";
    }
}
