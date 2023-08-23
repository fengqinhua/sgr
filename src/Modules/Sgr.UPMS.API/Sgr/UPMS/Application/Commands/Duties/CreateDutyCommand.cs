/**************************************************************
 * 
 * 唯一标识：9af1ba1f-b349-4ed7-9b94-9a36925b7e3e
 * 命名空间：Sgr.UPMS.Application.Commands.Duties
 * 创建时间：2023/8/23 18:07:31
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.UPMS.Application.Commands.Duties
{
    /// <summary>
    /// 创建职务
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望可以按名称创建一个职务
    /// </remarks>
    public class CreateDutyCommand
    {
        /// <summary>
        /// 职务名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;
        /// <summary>
        /// 职务备注
        /// </summary>
        public string? Remarks { get; set; }

        /// <summary>
        /// 组织标识
        /// </summary>
        public long OrgId { get; set; }
    }
}
