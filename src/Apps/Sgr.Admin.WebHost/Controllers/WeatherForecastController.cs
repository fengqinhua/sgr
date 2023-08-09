/**************************************************************
 * 
 * 唯一标识：48b98662-8a32-40aa-ba4c-463370fda8a8
 * 命名空间：Sgr.Admin.WebHost.Controllers
 * 创建时间：2023/8/9 16:48:17
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Sgr.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sgr.Admin.WebHost.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "获取天气列表")]
        //[Authorize]
        [AllowAnonymous]
        [ActionName("获取天气列表")]
        [AuditLogActionFilter()]
        //[TypeFilter(typeof(AuditLogActionFilterAttribute), Arguments = new object[] { "获取天气列表" })]
        //[ServiceFilter(typeof(AuditLogActionFilterAttribute))]
        //[UnAuditLogActionFilter()]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 
        /// </summary>
        public string? Summary { get; set; }
    }
}
