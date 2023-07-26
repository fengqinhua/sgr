/**************************************************************
 * 
 * 唯一标识：576766b7-cefc-421f-bc0c-470bb0df2508
 * 命名空间：Sgr
 * 创建时间：2023/7/20 21:17:42
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr
{
    /// <summary>
    /// 常量定义
    /// </summary>
    public static class Constant
    {
        /// <summary>
        /// 最小时间
        /// </summary>
        public static readonly DateTime MinDateTime = new DateTime(1900, 1, 1, 1, 1, 1);
        /// <summary>
        /// 最大时间
        /// </summary>
        public static readonly DateTime MaxDateTime = new DateTime(2999, 1, 1, 1, 1, 1);
        /// <summary>
        /// 实体的扩展属性最大长度
        /// </summary>
        public const int ExtendableObjectMaxLength = 2047;

        /// <summary>
        /// 缺省的组织标识
        /// </summary>
        public const long DEFAULT_ORGID = 11119;
        /// <summary>
        /// 缺省的用户标识
        /// </summary>
        public const long DEFAULT_USERID = 19527;

        //public const string Setup_SiteName = "SiteName";
        //public const string Setup_Language = "Language";
        //public const string Setup_AdminUsername = "AdminUsername";
        //public const string Setup_AdminUserId = "AdminUserId";
        //public const string Setup_AdminEmail = "AdminEmail";
        //public const string Setup_AdminPassword = "AdminPassword";
        //public const string Setup_DatabaseProvider = "DatabaseProvider";
        //public const string Setup_DatabaseConnectionString = "DatabaseConnectionString";
        //public const string Setup_DatabaseTablePrefix = "DatabaseTablePrefix";
        //public const string Setup_SiteTimeZone = "SiteTimeZone";
    }
}
