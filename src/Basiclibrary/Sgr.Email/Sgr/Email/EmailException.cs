using Sgr.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Email
{
    /// <summary>
    /// 发送邮件时发生的异常信息
    /// </summary>
    [Serializable]
    public class EmailException : BusinessException
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
