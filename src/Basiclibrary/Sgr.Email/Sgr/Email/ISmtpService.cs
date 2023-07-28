using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Email
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public interface ISmtpService
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="from"></param>
        /// <param name="sender"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="verificationType"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<SmtpResult> SendAsync(
                  string from,
                  string sender,
                  string host,
                  int port,
                  string userName,
                  string passWord,
                  string verificationType,
                  MailMessage message);
    }
}
