/**************************************************************
 * 
 * 唯一标识：80afea99-f675-4342-be5d-22696b7cf717
 * 命名空间：Sgr.Foundation.API.Application.DataCategory.Commands
 * 创建时间：2023/8/17 18:51:57
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Sgr.Domain.Entities;

namespace Sgr.DataCategories.Application.Commands
{
    public class UpdateCategpryItemCommand : IRequest<bool>
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 上级Id
        /// </summary>
        public long ParentId { get; set; } = 0;
        /// <summary>
        /// 字典项名称
        /// </summary>
        public string DcItemName { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;
        /// <summary>
        /// 状态
        /// </summary>
        public EntityStates State { get; set; } = EntityStates.Normal;


        public UpdateCategpryItemCommand() { }

        public UpdateCategpryItemCommand(long id, long parentId, string dcItemName, string? remarks, int orderNumber, EntityStates state)
            : this()
        {
            Id = id;
            ParentId = parentId;
            DcItemName = dcItemName;
            Remarks = remarks;
            OrderNumber = orderNumber;
            State = state;
        }
    }
}
