/**************************************************************
 * 
 * 唯一标识：e9dce53f-63d8-4e50-9d4a-1ff5eb7d8df9
 * 命名空间：Sgr.UPMS
 * 创建时间：2023/8/28 11:09:36
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Caching;

namespace Sgr.UPMS
{
    public class UpmsOptions
    {
        /// <summary>
        /// 允许连续输入密码错误的次数
        /// </summary>
        public int FailedPasswordAllowedAttempts { get; set; } = 0;
        /// <summary>
        /// 锁定账号的默认时间（分钟）
        /// </summary>
        public int FailedPasswordLockoutMinutes { get; set; } = 3;

        public static UpmsOptions CreatDefault()
        {
            return new UpmsOptions()
            {
                FailedPasswordAllowedAttempts = 0,
                FailedPasswordLockoutMinutes = 3
            };
        }
    }
}
