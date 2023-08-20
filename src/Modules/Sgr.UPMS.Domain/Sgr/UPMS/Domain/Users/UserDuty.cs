/**************************************************************
 * 
 * 唯一标识：1d339ad3-73ad-4cc7-9bb4-878406e66a5d
 * 命名空间：Sgr.UserAggregate
 * 创建时间：2023/8/8 9:03:51
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;

namespace Sgr.UPMS.Domain.Users
{
    /// <summary>
    /// 用户岗位
    /// </summary>
    public class UserDuty : EntityBase
    {
        /// <summary>
        /// 用户岗位
        /// </summary>
        protected UserDuty() { }
        /// <summary>
        /// 用户岗位
        /// </summary>
        /// <param name="dutyId"></param>
        public UserDuty(long dutyId) : this() { DutyId = dutyId; }

        /// <summary>
        /// 岗位标识
        /// </summary>
        public long DutyId { get; set; }
    }
}
