/**************************************************************
 * 
 * 唯一标识：e4ea987e-0e90-4e88-9f92-3c4936b84d5e
 * 命名空间：Sgr.Admin.WebHost.Tests.Sgr
 * 创建时间：2023/8/9 17:16:12
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Admin.WebHost.Tests.Sgr
{
    public class SgrAdminWebBaseTests
    {
        /// <summary>
        /// 构建服务HOST
        /// </summary>
        /// <returns></returns>
        public  TestServer CreateServer()
        {
            var factory = new SgrApplication();
            return factory.CreateServer();
        }

        public string WeatherForecast_Get => "/WeatherForecast";


        private class SgrApplication : WebApplicationFactory<Program>
        {
            public TestServer CreateServer()
            {
                return Server;
            }

            protected override IHost CreateHost(IHostBuilder builder)
            {
                builder.ConfigureAppConfiguration(c =>
                {
                    var directory = Path.GetDirectoryName(typeof(Program).Assembly.Location)!;
                    c.AddJsonFile(Path.Combine(directory, "appsettings.json"), optional: false);
                });

                return base.CreateHost(builder);
            }
        }
    }
}
