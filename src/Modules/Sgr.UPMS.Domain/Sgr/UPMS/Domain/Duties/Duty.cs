/**************************************************************
 * 
 * 唯一标识：cbacb81c-8d37-446a-af34-f52cff26a37a
 * 命名空间：Sgr.DutiesAggregate
 * 创建时间：2023/8/6 11:50:48
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities.Auditing;
using Sgr.Domain.Entities;
using Sgr.Exceptions;

namespace Sgr.UPMS.Domain.Duties
{
    /// <summary>
    /// 职务
    /// </summary>
    public class Duty : CreationAndModifyAuditedEntity<long, long>, IAggregateRoot, IOptimisticLock, IMustHaveOrg<long>, IHaveCode
    {

        private Duty()
        {
            State = EntityStates.Normal;
        }

        public Duty(string code, string name, int orderNumber, string? remarks, long orgId)
            :this()
        {
            Code = code;
            Name = name;
            OrderNumber = orderNumber;
            Remarks = remarks;
            OrgId = orgId;
        }



        /// <summary>
        /// 职务编码
        /// </summary>
        public string Code { get;  set; } = string.Empty;
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
        /// 职务状态
        /// </summary>
        public EntityStates State { get; internal protected set; } = EntityStates.Normal;

        /// <summary>
        /// 调整实体状态
        /// </summary>
        /// <param name="state"></param>
        /// <exception cref="BusinessException"></exception>
        public void ChangeEntityStates(EntityStates state)
        {
            this.State = state;
        }

        #region IOptimisticLock (乐观锁)

        /// <summary>
        /// 行版本
        /// </summary>
        public long RowVersion { get; set; }

        #endregion

        #region IMustHaveOrg  (如果一个实体实现该接口，那么必须存储实体所在组织的ID.)

        /// <summary>
        /// 所属组织
        /// </summary>
        public long OrgId { get; internal protected set; }

        #endregion
    }
}
