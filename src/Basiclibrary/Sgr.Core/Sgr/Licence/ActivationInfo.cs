/**************************************************************
 * 
 * 唯一标识：8c5aa64b-891c-49bb-8e3d-dfbc843bf47c
 * 命名空间：Sgr.Licence
 * 创建时间：2023/7/24 11:59:05
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Licence
{
    /// <summary>
    /// 激活信息
    /// </summary>
    internal class ActivationInfo
    {
        /// <summary>
        /// 注册码
        /// </summary>
        public string RegistrationCode { get; set; } = "";
        /// <summary>
        /// 授权开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = Constant.MaxDateTime;
        /// <summary>
        /// 授权结束时间
        /// </summary>
        public DateTime EndTime { get; set; } = Constant.MinDateTime;
        /// <summary>
        /// 授权数量
        /// </summary>
        public long NumOfLic { get; set; } = 0;
    }
}
