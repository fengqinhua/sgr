/**************************************************************
 * 
 * 唯一标识：deb736b2-681b-436a-8c3d-a57dbb5cdad8
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/9/7 15:36:44
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Sgr.BackGroundTasks
{
    public class HttpRequestArgs
    {
        /// <summary>
        /// 请求客户端名称
        /// </summary>
        public string ClientName { get; set; } = string.Empty;
        /// <summary>
        /// 确保请求成功，否则抛异常
        /// </summary>
        public bool EnsureSuccessStatusCode { get; set; } = true;

        /// <summary>
        /// 请求地址
        /// </summary>
        public string? RequestUrl { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;
        /// <summary>
        /// 请求报文体(统一为JSON格式)
        /// </summary>
        public string? Body { get; set; }
    }
}
