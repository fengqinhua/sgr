using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 获取配置参数
        /// <para>GetRequiredValue("Logging:LogLevel:Default")</para>
        /// <para>GetRequiredValue("User:0:Name")</para>
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string GetRequiredString(this IConfiguration configuration, string name) =>
            configuration[name] ?? throw new InvalidOperationException($"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ":" + name : name)}");

        /// <summary>
        /// 获取配置参数
        /// <para>GetRequiredValue("Logging:LogLevel:Default")</para>
        /// <para>GetRequiredValue("User:0:Name")</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetRequiredValue<T>(this IConfiguration configuration, string name, T defaultValue) =>
            configuration.GetValue<T>(name) ?? defaultValue;

        /// <summary>
        /// 获取配置参数
        /// <para>GetRequiredValue("Logging:LogLevel:Default")</para>
        /// <para>GetRequiredValue("User:0:Name")</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="name"></param>
        /// <param name="defaultValueFunc"></param>
        /// <returns></returns>
        public static T GetRequiredValue<T>(this IConfiguration configuration, string name, Func<T> defaultValueFunc) =>
            configuration.GetValue<T>(name) ?? defaultValueFunc();
    }
}
