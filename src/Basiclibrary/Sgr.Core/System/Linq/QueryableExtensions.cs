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
using Sgr.Application.ViewModels;
using Sgr.Domain.Entities;
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
        /// 排除已删除的
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> ExcludeDeleted<TEntity>(this IQueryable<TEntity> query)
            where TEntity : ISoftDelete
        {
            return query.Where(f => !f.IsDeleted);
        }

        /// <summary>
        /// 查询指定组织的数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="query"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> WithOrg<TEntity, TPrimaryKey>(this IQueryable<TEntity> query,
            TPrimaryKey orgId)
            where TEntity : IMustHaveOrg<TPrimaryKey>
        {
            return query.Where(f => f.OrgId!.Equals(orgId));
        }

        /// <summary>
        /// 查找指定组织的数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="query"></param>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> WithOrg<TEntity, TPrimaryKey>(this IQueryable<TEntity> query,
            IList<TPrimaryKey> orgIds)
            where TEntity : IMustHaveOrg<TPrimaryKey>
        {
            return query.Where(f => orgIds.Contains(f.OrgId));
        }



        /// <summary>
        ///  分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="skipCount"></param>
        /// <param name="takeCount"></param>
        /// <returns></returns>
        public static PagedResponse<T> ToPagedListBySkip<T>(this IQueryable<T> query,
            int skipCount,
            int takeCount)
        {
            Check.NotNull(query, nameof(query));

            if (skipCount < 0)
                skipCount = 0;

            long count = query.LongCount();

            if (skipCount >= count)
                return new PagedResponse<T>(count, null);   //return new PagedResponse(count, new BindingList<T>());
            else
            {
                var items = query.Skip(skipCount)
                 .Take(takeCount)
                 .ToList();

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
        public static PagedResponse<T> ToPagedListByPageSize<T>(this IQueryable<T> query,
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
