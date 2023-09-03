/**************************************************************
 * 
 * 唯一标识：43dd5f03-dc99-46a7-99cb-fa67e54c138e
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 12:14:35
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

namespace Sgr.BackGroundTasks
{
    /// <summary>
    /// 后台任务异常信息
    /// </summary>
    [Serializable]
    public class BackGroundTaskException : Exception
    {
        /// <summary>
        /// 后台任务异常信息
        /// </summary>
        /// <param name="message">错误消息文本内容.</param>
        public BackGroundTaskException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 后台任务异常信息
        /// </summary>
		/// <param name="messageFormat">错误消息文本内容模板.</param>
		/// <param name="args">错误消息文本内容参数.</param>
        public BackGroundTaskException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        /// <summary>
        /// 后台任务异常信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected BackGroundTaskException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 后台任务异常信息
        /// </summary>
        /// <param name="message">错误消息文本内容.</param>
        /// <param name="innerException">引发当前异常的异常信息.</param>
        public BackGroundTaskException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 异常数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public BackGroundTaskException WithData(string name, object value)
        {
            Data[name] = value;
            return this;
        }
    }

}
