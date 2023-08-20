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
using System.Collections.ObjectModel;
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

        private ConcurrentDictionary<Type, ConcurrentDictionary<string, Type>> _typeRegistrarInfo = new();

        /// <summary>
        /// 执行注册
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <typeparam name="TProvider"></typeparam>
        public void Register<TDbContext,TProvider>()
            where TDbContext : UnitOfWorkDbContext
            where TProvider : IEntityFrameworkTypeProvider, new()

        {
            Type tDbContext = typeof(TDbContext);
            Type tProvider = typeof(TProvider);

            if (!_typeRegistrarInfo.TryGetValue(tDbContext, out ConcurrentDictionary<string, Type>? dic))
            {
                dic = new();
                _typeRegistrarInfo.TryAdd(tDbContext, dic);
            }

            if (dic != null && !dic.ContainsKey(tProvider.FullName!))
                dic.TryAdd(tProvider.FullName!, tProvider);
        }

        /// <summary>
        /// 所有已注册的类型
        /// </summary>
        public IReadOnlyCollection<Type> GetAllTypeRegistrar<TDbContext>()
             where TDbContext : UnitOfWorkDbContext
        {
            Type tDbContext = typeof(TDbContext);

            if (_typeRegistrarInfo.TryGetValue(tDbContext, out ConcurrentDictionary<string, Type>? dic) && dic != null)
            {
                return dic.Values.ToList();
            }

            return new List<Type>();
        }
    }
}
