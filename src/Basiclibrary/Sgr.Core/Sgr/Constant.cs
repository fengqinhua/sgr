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
        public static readonly DateTimeOffset MinDateTime = new (1900, 1, 1, 1, 1, 1,TimeSpan.Zero);
        /// <summary>
        /// 最大时间
        /// </summary>
        public static readonly DateTimeOffset MaxDateTime = new (2999, 1, 1, 1, 1, 1, TimeSpan.Zero);
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
        /// <summary>
        /// 缺省的数据源名称
        /// </summary>
        public const string DEFAULT_DATABASE_SOURCE_NAME = "SGR_DATABASE_SOURCE";
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

        /// <summary>
        /// 用户标识
        /// </summary>
        public const string CLAIM_USER_ID = "use_id";
        /// <summary>
        /// 用户登录名称
        /// </summary>
        public const string CLAIM_LOGIN_NAME = "login_name";
        /// <summary>
        /// 用户姓名
        /// </summary>
        public const string CLAIM_USER_NAME = "use_name";
        /// <summary>
        /// 用户所属组织标识
        /// </summary>
        public const string CLAIM_USER_ORGID = "use_orgid";
        /// <summary>
        /// 在HttpContent中表示审计日志状态的标识Key
        /// </summary>
        public const string AUDITLOG_STATU_HTTPCONTEXT_KEY = "_SgrAuditLog";

        /// <summary>
        /// 
        /// </summary>
        public const string SGR_ERRORHANDLE_HEADERNAME = "_Sgr-Error-Handle";
        /// <summary>
        /// 
        /// </summary>
        public const string POWEREDBY_HEADERNAME  = "_X-Powered-By";
        /// <summary>
        /// 
        /// </summary>
        public const string POWEREDBY_HEADERVALUE = "sgr";


    }
}
