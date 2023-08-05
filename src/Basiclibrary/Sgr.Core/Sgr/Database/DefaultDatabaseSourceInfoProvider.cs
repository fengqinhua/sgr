/**************************************************************
 * 
 * 唯一标识：2dfd6d0f-dcaa-483f-8079-768b0644fc92
 * 命名空间：Sgr.Database
 * 创建时间：2023/8/4 17:01:51
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Sgr.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultDatabaseSourceInfoProvider : IDatabaseSourceInfoProvider
    {
        private readonly DatabaseSourceInfoOption _options;
        private readonly ILogger<DefaultDatabaseSourceInfoProvider> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public DefaultDatabaseSourceInfoProvider(IOptions<DatabaseSourceInfoOption> options,
            ILogger<DefaultDatabaseSourceInfoProvider> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DatabaseSourceInfo> ReadDatabaseSourceInfos()
        {
            var result = new List<DatabaseSourceInfo>();

            DatabaseType databaseType = getDatabaseType(_options.DatabaseType ?? "");
            string dataConnectionString = _options.DataConnectionString ?? ""   ;
            var commandTimeout = _options.CommandTimeout;

            if (databaseType != DatabaseType.NotSet && !string.IsNullOrEmpty(_options.DataConnectionString))
            {
                result.Add(new DatabaseSourceInfo(Constant.DEFAULT_DATABASE_SOURCE_NAME,
                    databaseType,
                    dataConnectionString,
                    commandTimeout.HasValue ? commandTimeout.Value : 10));

            }
            else
                _logger.LogWarning("无法从appsettings.json配置文件中获取正确的数据库连接信息");

            return result;
        }

        private DatabaseType getDatabaseType(string value)
        {
            if (value == "SqlServer")
                return DatabaseType.SqlServer; 
            else if (value == "Sqlite")
                return DatabaseType.Sqlite;
            else if (value == "MySQL")
                return DatabaseType.MySQL;
            else if (value == "PostgreSQL")
                return DatabaseType.PostgreSQL;
            else if (value == "Dameng")
                return DatabaseType.Dameng;
            else
                return DatabaseType.NotSet;
        }
    }
}
