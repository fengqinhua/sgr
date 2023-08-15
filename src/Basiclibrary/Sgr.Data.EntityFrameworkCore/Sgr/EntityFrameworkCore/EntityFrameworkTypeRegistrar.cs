/**************************************************************
 * 
 * 唯一标识：049bedeb-c2c1-4f1a-b088-24f656fb76a8
 * 命名空间：Sgr.EntityFrameworkCore
 * 创建时间：2023/8/5 18:10:24
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Licence;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityFrameworkCore
{
    /// <summary>
    /// 注册器
    /// </summary>
    public class EntityFrameworkTypeRegistrar
    {
        #region 单例模式

        /// <summary>
        /// 实例
        /// </summary>
        public static EntityFrameworkTypeRegistrar Instance { get { return Nested.instance; } }

        private EntityFrameworkTypeRegistrar() { }
        /// <summary>
        /// 通过嵌套类实现单利模式
        /// </summary>
        class Nested
        {
            static Nested()
            {
            }
            internal static readonly EntityFrameworkTypeRegistrar instance = new();
        }

        #endregion

        private ConcurrentDictionary<string, Type> _types = new();

        /// <summary>
        /// 执行注册
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        public void Register<TProvider>() where TProvider : IEntityFrameworkTypeProvider, new()
        {
            var type = typeof(TProvider);
            if (!string.IsNullOrEmpty(type.FullName) && !_types.ContainsKey(type.FullName))
                _types.TryAdd(type.FullName, type);
        }

        /// <summary>
        /// 所有已注册的类型
        /// </summary>
        public IReadOnlyCollection<Type> AllTypes
        {
            get
            {
                return _types.Values.ToList();
            }
        }
    }
}
