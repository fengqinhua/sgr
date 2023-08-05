/**************************************************************
 * 
 * 唯一标识：fdafba5d-f9e6-4676-bdd6-3ffe32dde7e6
 * 命名空间：Sgr.Database
 * 创建时间：2023/8/4 17:08:40
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sgr.Database
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultDatabaseSourceInfoManager : IDatabaseSourceInfoManager
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly Dictionary<string, DatabaseSourceInfo> _databaseSources = new Dictionary<string, DatabaseSourceInfo>(StringComparer.OrdinalIgnoreCase);
        private readonly IEnumerable<IDatabaseSourceInfoProvider> _databaseSourceProviders;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseSourceProviders"></param>
        public DefaultDatabaseSourceInfoManager(IEnumerable<IDatabaseSourceInfoProvider> databaseSourceProviders)
        {
            _databaseSourceProviders = databaseSourceProviders;

            this.Reload();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseSourceName"></param>
        /// <returns></returns>
        public DatabaseSourceInfo? GetDatabaseSourceInfo(string databaseSourceName = "")
        {
            if (!string.IsNullOrEmpty(databaseSourceName))
            {
                if (!this._databaseSources.TryGetValue(databaseSourceName, out DatabaseSourceInfo databaseSource))
                    return databaseSource;
            }

            if (this._databaseSources.TryGetValue(Constant.DEFAULT_DATABASE_SOURCE_NAME, out DatabaseSourceInfo default_databaseSource))
                return default_databaseSource;
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reload()
        {
            _lock.EnterWriteLock();
            try
            {
                _databaseSources.Clear();

                foreach (var item in this._databaseSourceProviders)
                {
                    var sources = item.ReadDatabaseSourceInfos();
                    foreach (var s in sources)
                    {
                        if (_databaseSources.ContainsKey(s.Name))
                            _databaseSources[s.Name] = s;
                        else
                            _databaseSources.Add(s.Name, s);
                    }
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
