/**************************************************************
 * 
 * 唯一标识：1fe88032-faec-43be-a942-8181f8092fec
 * 命名空间：Microsoft.Extensions.Logging
 * 创建时间：2023/8/10 21:05:05
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    ///  扩展
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="exception"></param>
        /// <param name="level"></param>
        public static void LogException(this ILogger logger, Exception exception, LogLevel level = LogLevel.Error)
        {

            var exStackTraceBuilder = new StringBuilder();
            exStackTraceBuilder.AppendLine("---------- Exception StackTrace ----------");
            exStackTraceBuilder.AppendLine(exception.StackTrace);

            var exDataBuilder = new StringBuilder();
            if(exception.Data != null && exception.Data.Count > 0)
            {
                exDataBuilder.AppendLine("---------- Exception Data ----------");
                foreach (var key in exception.Data.Keys)
                {
                    exDataBuilder.AppendLine($"{key} = {exception.Data[key]}");
                }
            }

            switch (level)
            {
                case LogLevel.Error:
                    logger.LogError($"Error Message = {exception.Message}");
                    logger.LogError(exStackTraceBuilder.ToString());
                    logger.LogError(exDataBuilder.ToString());
                    break;
                case LogLevel.Warning:
                    logger.LogWarning($"Error Message = {exception.Message}");
                    logger.LogWarning(exStackTraceBuilder.ToString());
                    logger.LogWarning(exDataBuilder.ToString());
                    break;
                case LogLevel.Information:
                    logger.LogInformation($"Error Message = {exception.Message}");
                    logger.LogInformation(exStackTraceBuilder.ToString());
                    logger.LogInformation(exDataBuilder.ToString());
                    break;
                case LogLevel.Trace:
                    logger.LogTrace($"Error Message = {exception.Message}");
                    logger.LogTrace(exStackTraceBuilder.ToString());
                    logger.LogTrace(exDataBuilder.ToString());
                    break;
                case LogLevel.Critical:
                    logger.LogCritical($"Error Message = {exception.Message}");
                    logger.LogCritical(exStackTraceBuilder.ToString());
                    logger.LogCritical(exDataBuilder.ToString());
                    break;
                default: // LogLevel.Debug || LogLevel.None
                    logger.LogDebug($"Error Message = {exception.Message}");
                    logger.LogDebug(exStackTraceBuilder.ToString());
                    logger.LogDebug(exDataBuilder.ToString());
                    break;
            }
        }
    }
}
