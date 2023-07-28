using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sgr.Email
{
    /// <summary>
    /// 邮件中的附件信息
    /// </summary>
    public class MailMessageAttachment
    {
        /// <summary>
        /// 获取或设置附件文件名
        /// </summary>
        public string Filename { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置附件文件流
        /// </summary>
        public Stream? Stream { get; set; }
    }
}
