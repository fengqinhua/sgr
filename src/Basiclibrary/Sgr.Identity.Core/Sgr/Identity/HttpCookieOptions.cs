/**************************************************************
 * 
 * 唯一标识：282515fb-b06b-46fb-ad87-5090e4a04b5e
 * 命名空间：Sgr.Identity
 * 创建时间：2023/8/21 20:26:12
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.Identity
{
    public class HttpCookieOptions
    {
        /// <summary>
        /// 设置存储用户登录信息的Cookie名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 设置存储用户登录信息（用户Token信息）的Cookie，无法通过客户端浏览器脚本(如JavaScript等)访问到
        /// </summary>
        public bool HttpOnly { get; set; } = true;
        /// <summary>
        /// 过期时间(秒),默认1个小时
        /// </summary>
        public int ExpireSeconds { get; set; } = 3600;
        /// <summary>
        /// 是否在过期时间过半的时候，自动延期
        /// </summary>
        public bool SlidingExpiration { get; set; } = true;
        /// <summary>
        /// 登录地址。认证失败，会自动跳转到这个地址
        /// </summary>
        public string LoginPath { get; set; } = string.Empty;

        /// <summary>
        /// 创建缺省配置项
        /// </summary>
        /// <returns></returns>
        public static HttpCookieOptions CreateDefault()
        {
            return new HttpCookieOptions()
            {
                Name = "sgrCookie",
                HttpOnly = true,
                ExpireSeconds = 3600,
                SlidingExpiration= true,
                LoginPath="/"
            };
        }
    }
}
