/**************************************************************
 * 
 * 唯一标识：fa1250e4-0af8-4664-9fec-02c5a21ddc16
 * 命名空间：Sgr.OrganizationAggregate
 * 创建时间：2023/8/3 10:03:13
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/


using Sgr.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.OrganizationAggregate
{
    /// <summary>
    /// 组织机构仓储
    /// </summary>
    public interface IOrganizationRepository : ITreeNodeBaseRepositoryOfTEntityAndTPrimaryKey<Organization, long>
    {
        /// <summary>
        /// 是否已存在某组织机构编码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="exclude_orgId"></param>
        /// <returns></returns>
        public Task<bool> ExistAsync(string code, long exclude_orgId = 0);
    }
}
