/**************************************************************
 * 
 * 唯一标识：11976fee-0f7b-406d-9939-e275e270f876
 * 命名空间：Sgr.Admin.WebHost.Controllers
 * 创建时间：2023/8/17 11:36:31
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Sgr.Admin.WebHost.Controllers
{
    /// <summary>
    /// 默认的Endpoint
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[AllowAnonymous]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            System.Reflection.Assembly assemFromType = typeof(ValuesController).Assembly;
            string version = $"{assemFromType.GetName().Version}";

            return new string[] { version, "it is all right ..." };
        }
    }
}
