/**************************************************************
 * 
 * 唯一标识：1846d38e-d41d-4d1f-93ef-c5cebca33390
 * 命名空间：Sgr.OrganizationAggregate
 * 创建时间：2023/8/2 11:29:14
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;
using Sgr.Domain.Entities.Auditing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Sgr.OrganizationAggregate
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public class Organization : CreationAndModifyAuditedEntity<long, long>, IAggregateRoot, IOptimisticLock, IExtendableObject, ITreeNode<long>
    {
        
        private Organization() { }

        /// <summary>
        /// 组织机构
        /// </summary>
        internal protected Organization(string core)
        {
            Code = core;
        }
    
        /// <summary>
        /// 组织机构编码（默认情况下，选用统一社会信用代码）
        /// </summary>
        public string Code { get; private set; } = string.Empty;
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 组织机构类型编码
        /// </summary>
        public string OrgTypeCode { get; internal protected set; } = string.Empty;
        /// <summary>
        /// 所属行政区划编码（默认情况下，选用邮政编码）
        /// </summary>
        public string AreaCode { get; internal protected set; } = string.Empty;
        /// <summary>
        /// 人员规模编码
        /// </summary>
        public string StaffSizeCode { get; set; } = string.Empty;

        /// <summary>
        /// 组织机构排序号
        /// </summary>
        public int OrderNumber { get; set; } = 0;
        /// <summary>
        /// Logo地址
        /// </summary>
        public string? LogoUrl { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Remarks { get; set; }

        /// <summary>
        /// 机构负责人/联系人
        /// </summary>
        public string? Leader { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// 所在地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 营业执照路径
        /// </summary>
        public string? BusinessLicensePath { get; set; }
        /// <summary>
        /// 组织机构是否已完成认证
        /// </summary>
        public bool IsConfirmed { get; private set; } = false;

        /// <summary>
        /// 组织机构状态
        /// </summary>
        public EntityStates State { get; internal protected set; } = EntityStates.Normal;

        #region  ITreeNode (树形结构)

        /// <summary>
        /// 上级组织Id
        /// </summary>
        public long ParentId { get; internal protected set; } = 0;
        /// <summary>
        /// 树节点层次目录
        /// </summary>
        public string NodePath { get; internal protected set; } = string.Empty;

        #endregion

        #region IOptimisticLock (乐观锁)

        /// <summary>
        /// 行版本
        /// </summary>
        public long RowVersion { get; set; }

        #endregion

        #region IExtendableObject (定义一个JSON格式的字符串属性来扩展对象/实体)

        /// <summary>
        /// 扩展信息 
        /// </summary>
        public string? ExtensionData { get; set; }

        #endregion

        //#region IMustHaveOrg  (如果一个实体实现该接口，那么必须存储实体所在组织的ID.)

        ///// <summary>
        ///// 所属组织
        ///// </summary>
        //public long OrgId { get; internal protected set; }

        //#endregion

        #region 业务规则

        ///// <summary>
        ///// 改变组织机构状态
        ///// </summary>
        ///// <param name="state"></param>
        //public void ChangeEntityState(EntityStates state)
        //{

        //}


        /// <summary>
        /// 是否为顶级组织
        /// </summary>
        /// <returns></returns>
        public bool IsRoot()
        {
            return ParentId == 0;
        }

        #endregion
    }
}
