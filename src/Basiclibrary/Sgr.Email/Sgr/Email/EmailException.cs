using System;

namespace Sgr.Email
{
    /// <summary>
    /// 发送邮件时发生的异常信息
    /// </summary>
    [Serializable]
    public class EmailException : Exception
    {
        /// <summary>
        /// 发送邮件时发生的异常信息
        /// </summary>
        /// <param name="message"></param>
        public EmailException(string message)
            : base(message)
        {

        }
    }
}
