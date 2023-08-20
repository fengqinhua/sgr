/**************************************************************
 * 
 * 唯一标识：520af65a-9c60-46c5-ac51-68f9d7582ab0
 * 命名空间：Sgr.Events
 * 创建时间：2023/8/7 21:20:09
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

//using MediatR;
//using Sgr.UserAggregate;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Sgr.Events
//{
//    /// <summary>
//    /// 超级管理员登录日志 
//    /// </summary>
//    public class SuperAdminLoginCompleteEvent : INotification 
//    {
//        /// <summary>
//        /// 登录Ip地址
//        /// </summary>
//        public string IpAddress { get; set; } = string.Empty;
//        /// <summary>
//        /// 登录平台
//        /// </summary>
//        public string LoginPlat { get; set; } = string.Empty;
//        /// <summary>
//        /// 登录账号
//        /// </summary>
//        public string LoginName { get; set; } = string.Empty;
//        /// <summary>
//        /// 用户姓名
//        /// </summary>
//        public string UserName { get; set; } = string.Empty;
//        /// <summary>
//        /// 登录时间
//        /// </summary>
//        public DateTimeOffset LoginTime { get; set; }

//        /// <summary>
//        /// 超级管理员登录日志
//        /// </summary>
//        /// <param name="loginName"></param>
//        /// <param name="userName"></param>
//        /// <param name="ipAddress"></param>
//        /// <param name="loginPlat"></param>
//        public SuperAdminLoginCompleteEvent(string loginName, string userName, string ipAddress, string loginPlat)
//        {
//            this.LoginName = loginName;
//            this.UserName = userName;
//            this.LoginPlat = loginPlat;
//            this.IpAddress = ipAddress;

//            this.LoginTime = DateTimeOffset.Now;
//        }

//    }

//    /// <summary>
//    /// 普通用户登录完成事件
//    /// </summary>
//    public class UserLoginCompleteEvent : SuperAdminLoginCompleteEvent
//    {
//        /// <summary>
//        /// 组织标识
//        /// </summary>
//        public long OrgId { get; set; }

//        /// <summary>
//        /// 普通用户登录完成事件
//        /// </summary>
//        /// <param name="orgId"></param>
//        /// <param name="loginName"></param>
//        /// <param name="userName"></param>
//        /// <param name="ipAddress"></param>
//        /// <param name="loginPlat"></param>
//        public UserLoginCompleteEvent(long orgId,string loginName, string userName, string ipAddress, string loginPlat)
//            : base(loginName, userName, ipAddress, loginPlat)
//        {
//            this.OrgId = orgId;
//        }


//    }
//}
