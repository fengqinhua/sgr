/**************************************************************
 * 
 * 唯一标识：002141cd-3806-42f1-acb0-a9f5fa0099ee
 * 命名空间：Sgr.Middlewares
 * 创建时间：2023/8/8 15:23:12
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.AspNetCore.Middlewares.PoweredBy
{
    internal class PoweredByMiddlewareOptions : IPoweredByMiddlewareOptions
    {

        public string HeaderName => Constant.POWEREDBY_HEADERNAME;
        public string HeaderValue { get; set; } = Constant.POWEREDBY_HEADERVALUE;

        public bool Enabled { get; set; } = true;
    }
}
