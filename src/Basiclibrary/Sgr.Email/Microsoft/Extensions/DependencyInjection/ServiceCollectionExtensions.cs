using Sgr.Email;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加邮件发送功能
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEmail(this IServiceCollection services)
        {
            services.AddTransient<IEmailAddressValidator, EmailAddressValidator>();
            services.AddTransient<ISmtpService, SmtpService>();

            return services;
        }
    }
}
