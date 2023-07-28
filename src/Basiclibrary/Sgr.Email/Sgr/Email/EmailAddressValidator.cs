using MimeKit;

namespace Sgr.Email
{
    /// <summary>
    /// 电子邮件地址验证服务实现类
    /// </summary>
    public class EmailAddressValidator : IEmailAddressValidator
    {
        /// <summary>
        /// 验证 电子邮件地址
        /// </summary>
        /// <param name="emailAddress"> 电子邮件地址</param>
        /// <returns>如果电子邮件有效，则为true，否则为false.</returns>
        public bool Validate(string emailAddress)
            => emailAddress?.IndexOf('@') > -1 && MailboxAddress.TryParse(emailAddress, out _);
    }
}
