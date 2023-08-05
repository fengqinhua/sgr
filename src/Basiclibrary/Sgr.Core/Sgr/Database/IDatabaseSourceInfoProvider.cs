/**************************************************************
 * 
 * 唯一标识：39c14429-6eb8-466b-a7b4-6a8fb79f479b
 * 命名空间：Sgr.Database
 * 创建时间：2023/8/4 16:59:25
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
    /// 数据源提供者
    /// </summary>
    public interface IDatabaseSourceInfoProvider
    {
        /// <summary>
        /// 读取数据源
        /// </summary>
        /// <returns></returns>
        IEnumerable<DatabaseSourceInfo> ReadDatabaseSourceInfos();
    }
}
