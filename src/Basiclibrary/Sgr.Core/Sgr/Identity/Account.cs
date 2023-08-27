/**************************************************************
 * 
 * 唯一标识：1e209ba3-b574-4438-baaf-a9292b0b1e86
 * 命名空间：Sgr.Identity
 * 创建时间：2023/8/21 16:22:32
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Identity
{
    /// <summary>
    /// 账号信息
    /// </summary>
    public class Account
    {
       public Account(string id, string orgId, string loginName)
        {
            Id = id;
            OrgId = orgId;
            LoginName = loginName;
        }

        /// <summary>
        /// 账号标识
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// 账号所属组织标识
        /// </summary>
        public string OrgId { get; }
        /// <summary>
        /// 账号登录名称
        /// </summary>
        public string LoginName { get; }

        ///// <summary>
        ///// 用户姓名
        ///// </summary>
        //public string? UserName { get; set; }
        ///// <summary>
        ///// 用户邮箱
        ///// </summary>
        //public string? UserEmail { get; set; }
        ///// <summary>
        ///// 用户电话
        ///// </summary>
        //public string? UserPhone { get; set; }
    }
}
