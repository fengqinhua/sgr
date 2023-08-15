using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Database
{
    public interface IDataBaseInitialize
    {
        /// <summary>
        /// 获取IStartup执行顺序，默认值为0。
        /// </summary>
        int Order { get; }
        /// <summary>
        /// 执行数据库初始化
        /// </summary>
        Task Initialize();
    }
}
