/**************************************************************
 * 
 * 唯一标识：075ccd48-05cf-4f92-83f6-aa1f0be2635a
 * 命名空间：Sgr.BackGroundTasks
 * 创建时间：2023/8/31 17:53:44
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.BackGroundTasks
{
    public class RemoveCacheArgs
    {
        /// <summary>
        /// 待清理的缓存键值KEY
        /// </summary>
        public string? Key { get; set; }
        /// <summary>
        /// 待清理的缓存键值前缀
        /// </summary>
        public string? Prefix { get; set; }
    }
}
