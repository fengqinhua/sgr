using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Email
{
    /// <summary>
    /// 邮件服务实现类
    /// </summary>
    public class SmtpService : ISmtpService
    {
        private static readonly char[] EmailsSeparator = new char[] { ',', ';' };

        private readonly ILogger _logger;

        /// <summary>
        /// 邮件服务实现类
        /// </summary>
        /// <param name="logger"></param>
        public SmtpService(ILogger<SmtpService> logger)
        {
            _logger = logger;
        }

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
        public async Task<SmtpResult> SendAsync(
            string from,
            string sender,
            string host,
            int port,
            string userName,
            string passWord,
            string verificationType,
            MailMessage message)
        {
            try
            {
                var mimeMessage = FromMailMessage(from, sender, message);
                await SendOnlineMessage(host, port, userName, passWord, verificationType, mimeMessage);

                return SmtpResult.Success;
            }
            catch (Exception ex)
            {
                return SmtpResult.Failed($"An error occurred while sending an email: '{ex.Message}'");
            }

        }

        private MimeMessage FromMailMessage(
            string from,
            string sender,
            MailMessage message)
        {
            var mimeMessage = new MimeMessage
            {
                Sender = new MailboxAddress(sender, from)
            };
            //添加发件人信息
            mimeMessage.From.Add(new MailboxAddress(sender, from));
            //添加收件人信息
            if (!string.IsNullOrWhiteSpace(message.To))
            {
                foreach (var address in message.To.Split(EmailsSeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    mimeMessage.To.Add(MailboxAddress.Parse(address));
                }
            }
            //添加抄送（CC）信息
            if (!string.IsNullOrWhiteSpace(message.Cc))
            {
                foreach (var address in message.Cc.Split(EmailsSeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    mimeMessage.Cc.Add(MailboxAddress.Parse(address));
                }
            }
            //添加秘送（BCC）信息
            if (!string.IsNullOrWhiteSpace(message.Bcc))
            {
                foreach (var address in message.Bcc.Split(EmailsSeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    mimeMessage.Bcc.Add(MailboxAddress.Parse(address));
                }
            }

            foreach (var address in mimeMessage.From)
            {
                mimeMessage.ReplyTo.Add(address);
            }

            //邮件主题
            mimeMessage.Subject = message.Subject;

            var body = new BodyBuilder();

            if (message.IsBodyHtml)
            {
                body.HtmlBody = message.Body;
            }

            if (message.IsBodyText)
            {
                body.TextBody = message.BodyText;
            }

            foreach (var attachment in message.Attachments)
            {
                if (attachment.Stream != null)
                {
                    body.Attachments.Add(attachment.Filename, attachment.Stream);
                }
            }

            mimeMessage.Body = body.ToMessageBody();

            return mimeMessage;
        }

        private async Task SendOnlineMessage(
            string host,
            int port,
            string userName,
            string passWord,
            string verificationType,
            MimeMessage message)
        {

            var secureSocketOptions = SecureSocketOptions.Auto;
            if (verificationType == "NULL")
                secureSocketOptions = SecureSocketOptions.None;
            else if (verificationType == "SSL")
                secureSocketOptions = SecureSocketOptions.SslOnConnect;
            else if (verificationType == "TLS")
                secureSocketOptions = SecureSocketOptions.StartTls;

            using (var client = new SmtpClient())
            {
                client.Timeout = 20000;
                client.ServerCertificateValidationCallback = CertificateValidationCallback;
                await client.ConnectAsync(host, port, secureSocketOptions);
                await client.AuthenticateAsync(userName, passWord);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            _logger.LogError(string.Concat("SMTP Server's certificate {CertificateSubject} issued by {CertificateIssuer} ",
                "with thumbprint {CertificateThumbprint} and expiration date {CertificateExpirationDate} ",
                "is considered invalid with {SslPolicyErrors} policy errors"),
                certificate.Subject, certificate.Issuer, certificate.GetCertHashString(),
                certificate.GetExpirationDateString(), sslPolicyErrors);

            if (sslPolicyErrors.HasFlag(SslPolicyErrors.RemoteCertificateChainErrors) && chain?.ChainStatus != null)
            {
                foreach (var chainStatus in chain.ChainStatus)
                {
                    _logger.LogError("Status: {Status} - {StatusInformation}", chainStatus.Status, chainStatus.StatusInformation);
                }
            }

            //return false;
            return true; //始终都返回True，不验证证书有效性
        }
    }
}
