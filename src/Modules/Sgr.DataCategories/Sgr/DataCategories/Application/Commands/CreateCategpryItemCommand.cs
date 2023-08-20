/**************************************************************
 * 
 * 唯一标识：5241d768-ae2b-4992-953b-7ed8de39a34d
 * 命名空间：Sgr.DataCategories.Application.Commands
 * 创建时间：2023/8/17 18:07:06
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;

namespace Sgr.DataCategories.Application.Commands
{
    public class CreateCategpryItemCommand : IRequest<bool>
    {
        /// <summary>
        /// 上级Id
        /// </summary>
        public long ParentId { get; set; } = 0;
        /// <summary>
        /// 字典分类标识
        /// </summary>
        public string CategoryTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// 字典项名称
        /// </summary>
        public string DcItemName { get; set; } = string.Empty;
        /// <summary>
        /// 字典项值
        /// </summary>
        public string DcItemValue { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;

        public CreateCategpryItemCommand()
        {

        }

        public CreateCategpryItemCommand(long parentId,
            string categoryTypeCode,
            string dcItemName,
            string dcItemValue,
            string? remarks,
            int orderNumber) : this()
        {
            ParentId = parentId;
            CategoryTypeCode = categoryTypeCode;
            DcItemName = dcItemName;
            DcItemValue = dcItemValue;
            Remarks = remarks;
            OrderNumber = orderNumber;
        }
    }
}
