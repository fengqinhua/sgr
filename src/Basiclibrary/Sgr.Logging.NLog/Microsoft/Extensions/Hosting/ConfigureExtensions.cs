/**************************************************************
 * 
 * 唯一标识：964c620c-6b8f-4b3d-bce4-25d9047cd7cc
 * 命名空间：Microsoft.Extensions.Hosting
 * 创建时间：2023/8/1 8:37:38
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using NLog;
using NLog.Config;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConfigureExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostBuilder UseNLogWeb(this IHostBuilder builder)
        {
            if (NLog.LogManager.Configuration == null
                || NLog.LogManager.Configuration.LoggingRules.Count == 0)
            {
                var config = new NLog.Config.LoggingConfiguration();

                // Targets where to log to: File and Console
                var logfile = new NLog.Targets.FileTarget("logfile")
                {
                    Name = "appLogfile",
                    FileName = "logs/sgr-nlog-${shortdate}.log",
                    ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.DateAndSequence,
                    ArchiveAboveSize = 5000000,
                    Layout = "${longdate} | ${level:uppercase=true} |  ${message}  ${exception:format=tostring}"
                };

                var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

                // Rules for mapping loggers to targets            
                config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
                config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

                // Apply config           
                NLog.LogManager.Configuration = config;
            }

#if DEBUG
            foreach(var rule in NLog.LogManager.Configuration.LoggingRules)
            {
                bool needChange = false;
                if(!rule.Levels.Contains(LogLevel.Debug))
                {
                    foreach (var target in rule.Targets)
                    {
                        if (target.Name == "appLogfile")
                        {
                            needChange = true;
                            break;
                        }
                    }
                }

                if (needChange)
                    rule.EnableLoggingForLevels(LogLevel.Debug, LogLevel.Fatal);

            }

            LogManager.ReconfigExistingLoggers(); // Soft refresh
#endif


            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("nlog configuration complete!");


            builder.UseNLog();
            
            return builder;
        }
    }
}
