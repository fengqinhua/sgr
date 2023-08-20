/**************************************************************
 * 
 * 唯一标识：5746156a-071f-4da7-acca-523083cae947
 * 命名空间：Sgr
 * 创建时间：2023/8/20 18:26:17
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.AspNetCore.Modules;

namespace Sgr
{
    public class WebApplicationAddOptions
    {
        public WebApplicationAddOptions() {
            ModuleInfoProvider = null;
#if DEBUG
            IsAuditFull = true;
#else
            IsAuditFull = false;
#endif
        }
        /// <summary>
        /// 模块信息提供者
        /// </summary>
        public IModuleInfoProvider? ModuleInfoProvider { get; set; }
        /// <summary>
        /// 审计时是否记录完整的信息（完成信息包括客户端浏览器、OS、请求参数、请求耗时等信息）
        /// </summary>
        public bool IsAuditFull { get; set; }
    }
}

