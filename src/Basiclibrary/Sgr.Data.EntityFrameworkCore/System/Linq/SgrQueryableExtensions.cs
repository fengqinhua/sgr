/**************************************************************
 * 
 * 唯一标识：8e44bc43-85a2-4806-902c-fd5c232124e7
 * 命名空间：System.Linq
 * 创建时间：2023/8/16 20:18:21
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Sgr;
using Sgr.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    /// <summary>
    /// IQueryable 接口扩展
    /// </summary>
    public static class SgrQueryableExtensions
    {
        /// <summary>
        ///  分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="skipCount"></param>
        /// <param name="takeCount"></param>
        /// <returns></returns>
        public static async Task<PagedResponse<T>> ToPagedListBySkipAsync<T>(this IQueryable<T> query,
            int skipCount,
            int takeCount)
        {
            Check.NotNull(query, nameof(query));

            if (skipCount < 0)
                skipCount = 0;

            long count = await query.LongCountAsync();

            if (skipCount >= count)
                return new PagedResponse<T>(count, null);   //return new PagedResponse(count, new BindingList<T>());
            else
            {
                var items = await query.Skip(skipCount)
                    .Take(takeCount)
                    .ToListAsync();

                return new PagedResponse<T>(count, items);  //return new Tuple<IEnumerable<T>, long>(items, count);
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
        public static Task<PagedResponse<T>> ToPagedListByPageSizeAsync<T>(this IQueryable<T> query,
             int pageIndex,
             int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;

            int skipCount = (pageIndex - 1) * pageSize;

            return ToPagedListBySkipAsync(query, skipCount, pageSize);
        }
    }
}
