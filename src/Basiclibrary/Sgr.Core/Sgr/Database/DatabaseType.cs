/**************************************************************
 * 
 * 唯一标识：c2450f14-c6ad-4c2c-ab7f-03c608ab9f50
 * 命名空间：Sgr.Database
 * 创建时间：2023/8/4 16:55:21
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Database
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// SqlServer
        /// </summary>
        SqlServer = 1,
        /// <summary>
        /// Sqlite
        /// </summary>
        Sqlite = 11,
        /// <summary>
        /// MySQL
        /// </summary>
        MySQL = 21,
        /// <summary>
        /// PostgreSQL
        /// </summary>
        PostgreSQL = 31,
        /// <summary>
        /// Dameng
        /// </summary>
        Dameng = 61
    }
}
