/**************************************************************
 * 
 * 唯一标识：088cbf60-3525-4de5-a7b1-8f5392573c64
 * 命名空间：Sgr.UPMS.Application.Commands.Duties
 * 创建时间：2023/8/23 18:09:33
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

namespace Sgr.UPMS.Application.Commands.Duties
{
    /// <summary>
    /// 修改职务信息
    /// </summary>
    /// <remarks>
    /// 用户故事： 作为组织机构管理人员，希望可以修改某一个已存在的职务的基本信息
    /// </remarks>
    public class UpdateDutyCommand
    {
        /// <summary>
        /// 职务标识
        /// </summary>
        public long DutyId { get; set; }

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

    }
}
