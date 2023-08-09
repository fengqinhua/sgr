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

            if(_context.HttpContext == null)
                throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// 当前用户标识
        /// </summary>
        public string Id => _context.HttpContext!.GetValueFromClaim(Constant.CLAIM_USER_ID, "");

        /// <summary>
        /// 当前用户登录名称
        /// </summary>
        public string LoginName => _context.HttpContext!.GetValueFromClaim(Constant.CLAIM_LOGIN_NAME, "unset");
        /// <summary>
        /// 当前用户姓名
        /// </summary>
        public string UserName => _context.HttpContext!.GetValueFromClaim(Constant.CLAIM_USER_NAME, "");

        /// <summary>
        /// 当前用户所在组织
        /// </summary>
        public string OrgId => _context.HttpContext!.GetValueFromClaim(Constant.CLAIM_USER_ORGID, "0");

    }
}
