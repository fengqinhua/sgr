/**************************************************************
 * 
 * 唯一标识：cd8e7052-d538-4896-a699-bbe3cc1419c1
 * 命名空间：Sgr.Application.ViewModels
 * 创建时间：2023/8/16 17:02:06
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Application.ViewModels
{
    [Serializable]
    public class PagedRequest
    {
        public PagedRequest()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
            //this.SearchText = "";
            this.SortName = "Id";
            this.IsAscending = false;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
        ///// <summary>
        ///// 查询字符
        ///// </summary>
        //public string SearchText { get; set; }
        /// <summary>
        /// 排序字段名称
        /// </summary>
        public string SortName { get; set; }
        /// <summary>
        /// 是否升序
        /// </summary>
        public bool IsAscending { get; set; }
    }
}
