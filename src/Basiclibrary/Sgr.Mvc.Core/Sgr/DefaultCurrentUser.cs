/**************************************************************
 * 
 * 唯一标识：e50e412f-182c-402e-be6d-32bf1cb06c76
 * 命名空间：Sgr
 * 创建时间：2023/8/4 22:02:56
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Http;
using System;
using System.IO.Compression;

namespace Sgr
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultCurrentUser : ICurrentUser
    {
        private IHttpContextAccessor _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DefaultCurrentUser(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// 
        /// </summary>
        public string Id => getValueFromClaim("use_id", "");

        /// <summary>
        /// 
        /// </summary>
        public string Name => getValueFromClaim("use_name", "unset");

        /// <summary>
        /// 
        /// </summary>
        public string OrgId => getValueFromClaim("use_orgid", "unset");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        protected string getValueFromClaim(string key,string defaultValue)
        {
            var claim = _context.HttpContext!.User.FindFirst(key);
            if (claim != null)
                return claim.Value;
            return defaultValue;
        }
    }
}
