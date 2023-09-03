/**************************************************************
 * 
 * 唯一标识：85c4ccc6-b2b5-453f-bc1d-71726f2c44ee
 * 命名空间：Sgr.Admin.WebHost.Tests.Sgr.Middlewares
 * 创建时间：2023/8/9 17:13:15
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Admin.WebHost.Tests.Sgr.Middlewares
{

    [TestClass]
    public class PoweredByMiddlewareTests: SgrAdminWebBaseTests
    {
        public static string WeatherForecast_Get => "/WeatherForecast";

        [TestMethod]
        public async Task CheckPoweredBy()
        {
            var response = await base.TestServer.CreateClient().GetAsync(WeatherForecast_Get);
            var values = response.Headers.GetValues(Constant.POWEREDBY_HEADERNAME);
            Assert.AreEqual(values?.First(), Constant.POWEREDBY_HEADERVALUE);
        }
    }
}
