/**************************************************************
 * 
 * 唯一标识：f04bca57-609d-4a01-aa06-a4c137194ba1
 * 命名空间：Sgr.Admin.WebHost.Controllers
 * 创建时间：2023/8/21 20:09:25
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Sgr.Admin.WebHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesController
    {
        /// <summary>
        /// 获取当前系统时间
        /// </summary>
        /// <returns></returns>
        [HttpGet("UtcNow")]
        public string GetUtcNow()
        {
            return DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }

        [Authorize]
        [HttpGet("TimeZone")]
        public string GetTimeZone()
        {
            return TimeZoneInfo.Local.StandardName;
        }


    }
}
