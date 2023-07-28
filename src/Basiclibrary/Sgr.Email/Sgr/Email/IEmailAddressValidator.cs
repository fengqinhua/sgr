using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Email
{
    /// <summary>
    /// 电子邮件地址验证服务
    /// </summary>
    public interface IEmailAddressValidator
    {
        /// <summary>
        /// 验证 电子邮件地址
        /// </summary>
        /// <param name="emailAddress"> 电子邮件地址</param>
        /// <returns>如果电子邮件有效，则为true，否则为false.</returns>
        bool Validate(string emailAddress);
    }
}
