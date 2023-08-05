/**************************************************************
 * 
 * 唯一标识：31b59344-37a5-49e0-b9ad-4f908ab5105e
 * 命名空间：Sgr.Database
 * 创建时间：2023/8/4 17:01:17
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
    /// 
    /// </summary>
    public class DatabaseSourceInfoOption
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string? DatabaseType { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string? DataConnectionString { get; set; }
        /// <summary>
        /// 超时时间
        /// </summary>
        public int? CommandTimeout { get; set; }
    }
}
