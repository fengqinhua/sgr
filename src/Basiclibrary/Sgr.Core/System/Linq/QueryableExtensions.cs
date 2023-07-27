/**************************************************************
 * 
 * 唯一标识：bb15e008-975c-4780-b218-338917d1672a
 * 命名空间：System.Linq
 * 创建时间：2023/7/27 21:19:01
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace System.Linq
{
    /// <summary>
    /// IQueryable 接口扩展
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        ///  分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="skipCount"></param>
        /// <param name="takeCount"></param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T>, long> ToPagedListBySkip<T>(this IQueryable<T> query,
            int skipCount,
            int takeCount)
        {
            Check.NotNull(query, nameof(query));

            if (skipCount < 0)
                skipCount = 0;

            long count = query.LongCount();

            if (skipCount >= count)
                return new Tuple<IEnumerable<T>, long>(new BindingList<T>(), count);
            else
            {
                var items = query.Skip(skipCount)
                 .Take(takeCount)
                 .ToList();
                return new Tuple<IEnumerable<T>, long>(items, count);
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static Tuple<IEnumerable<T>, long> ToPagedListByPageSize<T>(this IQueryable<T> query,
             int pageIndex,
             int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;

            int skipCount = (pageIndex - 1) * pageSize;

            return ToPagedListBySkip(query, skipCount, pageSize);
        }



    }
}
