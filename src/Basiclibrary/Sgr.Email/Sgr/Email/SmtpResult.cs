using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Email
{
    /// <summary>
    /// 邮件发送返回值
    /// </summary>
    public class SmtpResult
    {
        /// <summary>
        /// 邮件发送时的错误集合
        /// </summary>
        public IEnumerable<string>? Errors { get; protected set; }

        /// <summary>
        /// 邮件是否发送成功
        /// </summary>
        public bool Succeeded { get; protected set; }

        /// <summary>
        /// 邮件发送失败
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static SmtpResult Failed(params string[] errors) => new SmtpResult { Succeeded = false, Errors = errors };

        /// <summary>
        /// 邮件发送成功
        /// </summary>
        public static SmtpResult Success { get; } = new SmtpResult { Succeeded = true };
    }
}
