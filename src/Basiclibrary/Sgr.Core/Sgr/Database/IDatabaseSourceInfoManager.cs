/**************************************************************
 * 
 * 唯一标识：dbdd94a4-d976-4139-bd02-497e2eb67f6c
 * 命名空间：Sgr.Database
 * 创建时间：2023/8/4 17:00:04
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
    /// 数据源管理器
    /// </summary>
    public interface IDatabaseSourceInfoManager
    {
        /// <summary>
        /// 重新加载数据源信息
        /// </summary>
        void Reload();
        /// <summary>
        /// 获取数据源信息
        /// </summary>
        /// <param name="databaseSourceName"></param>
        /// <returns></returns>
        DatabaseSourceInfo? GetDatabaseSourceInfo(string databaseSourceName="");
    }
}
