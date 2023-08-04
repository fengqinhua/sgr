/**************************************************************
 * 
 * 唯一标识：ec95aba9-88df-40c4-a0eb-aee591a45702
 * 命名空间：Sgr.OrganizationAggregate
 * 创建时间：2023/8/3 10:05:29
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using Sgr.Domain.Entities;
using Sgr.Domain.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.OrganizationAggregate
{
    /// <summary>
    /// 领域服务：组织机构管理
    /// </summary>
    public interface IOrganizationManage : IDomainManager
    {
        /// <summary>
        /// 创建一个新的组织机构
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="orgTypeCode"></param>
        /// <param name="areaCode"></param>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        Task<Organization> CreateNewAsync(
            string code,
            string name,
            string orgTypeCode,
            string areaCode,
            long ParentId = 0);

        /// <summary>
        /// 调整组织机构的父节点
        /// </summary>
        /// <param name="org"></param>
        /// <param name="newParentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ChangeParentIdAsync(
            Organization org,
            long newParentId,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 是否符合组织编码规范要求
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BusinessCheckResult> IsUniqueAsync(
            string code,
            long id = 0,
            CancellationToken cancellationToken = default);


    }
}
