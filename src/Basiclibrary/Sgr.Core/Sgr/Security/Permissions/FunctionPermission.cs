/**************************************************************
 * 
 * 唯一标识：1e5aa8a7-f209-46d3-b332-3ae6b1afab79
 * 命名空间：Sgr.Security.Permissions
 * 创建时间：2023/8/22 11:00:47
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Sgr.Security.Permissions
{
    public class FunctionPermission
    {
        public FunctionPermission(string name,
            string category,
            string description = "",
            bool onlyGrantToSuperAdmin = false)
        {
            if (string.IsNullOrEmpty(name))
                throw new BusinessException("Function Permission Name Is Null Or Empty");

            if (string.IsNullOrEmpty(category))
                throw new BusinessException("Function Permission Category Is Null Or Empty");

            Name = name;
            Category = category;
            Description = string.IsNullOrEmpty(description) ? name : description;
            OnlyGrantToSuperAdmin = onlyGrantToSuperAdmin;
        }

        /// <summary>
        /// 功能权限项名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 功能权限项描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 功能权限项分类
        /// </summary>
        public string Category { get; set; } 
        /// <summary>
        /// 仅限授权给超级管理员
        /// </summary>
        public bool OnlyGrantToSuperAdmin { get; set; }
    }
}
