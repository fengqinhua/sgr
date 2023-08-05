/**************************************************************
 * 
 * 唯一标识：8b6929c1-3b09-4c77-9add-cace19ba7b51
 * 命名空间：Sgr.Database
 * 创建时间：2023/8/4 16:56:53
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Sgr.Database
{
    /// <summary>
    /// 数据源信息
    /// </summary>
    public class DatabaseSourceInfo
    {
        /// <summary>
        /// 数据源信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="databaseType"></param>
        /// <param name="dataConnectionString"></param>
        /// <param name="commandTimeout"></param>
        public DatabaseSourceInfo(
            string name, 
            DatabaseType databaseType, 
            string dataConnectionString, 
            int commandTimeout = 10)
        {
            this.Name = name;
            this.DatabaseType = databaseType;
            this.DataConnectionString = dataConnectionString;
            this.CommandTimeout = commandTimeout;
        }
        /// <summary>
        /// 数据库配置名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DatabaseType { get; private set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DataConnectionString { get; private set; }
        /// <summary>
        /// 超时时间
        /// </summary>
        public int CommandTimeout { get; private set; }
    }
}
