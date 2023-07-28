using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Email
{
    /// <summary>
    /// 邮件信息
    /// </summary>
    public class MailMessage
    {
        /// <summary>
        /// 获取或设置收件人，多个使用,分隔
        /// </summary>
        public string To { get; set; } = string.Empty;
        /// <summary>
        /// 获取或设置抄送电子邮件，多个使用,分隔
        /// </summary>
        public string Cc { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置密送人电子邮件，多个使用,分隔
        /// </summary>
        public string Bcc { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置邮件主题.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置邮件正文.
        /// </summary>
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置消息内容为纯文本
        /// </summary>
        public string BodyText { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置邮件正文是否为 HTML.
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// 获取或设置消息正文是否为纯文本
        /// </summary>
        public bool IsBodyText { get; set; }

        /// <summary>
        /// 邮件中的附件集合
        /// </summary>
        public List<MailMessageAttachment> Attachments { get; } = new List<MailMessageAttachment>();
    }
}
