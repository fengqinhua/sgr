/**************************************************************
 * 
 * 唯一标识：b784ad98-b7c2-43a2-b4bf-70acd32af6b6
 * 命名空间：Sgr.OrganizationAggregate
 * 创建时间：2023/8/3 10:07:10
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;
using Sgr.Domain.Managers;
using Sgr.Exceptions;
using Sgr.Generator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.OrganizationAggregate
{
    /// <summary>
    /// 组织编码检查
    /// </summary>
    public class OrganizationManage : TreeNodeManageBase<Organization, long>,IOrganizationManage
    {
        private readonly IOrganizationChecker _organizationChecker;
        private readonly INumberIdGenerator _numberIdGenerator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationRepository"></param>
        /// <param name="organizationChecker"></param>
        /// <param name="numberIdGenerator"></param>
        public OrganizationManage(IOrganizationRepository organizationRepository,
            IOrganizationChecker organizationChecker,
            INumberIdGenerator numberIdGenerator)
            : base(organizationRepository)
        {
            _organizationChecker = organizationChecker;
            _numberIdGenerator = numberIdGenerator;
        }

        /// <summary>
        /// 创建一个新的组织机构
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="orgTypeCode"></param>
        /// <param name="areaCode"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task<Organization> CreateNewAsync(
            string code,
            string name,
            string orgTypeCode,
            string areaCode,
            long parentId = 0)
        {
            //不可为空
            Check.StringNotNullOrWhiteSpace(code, nameof(code));
            Check.StringNotNullOrWhiteSpace(name, nameof(name));
            Check.StringNotNullOrWhiteSpace(orgTypeCode, nameof(orgTypeCode));
            Check.StringNotNullOrWhiteSpace(areaCode, nameof(areaCode));

            //检查组织机构编码是否符合规范
            if (await _organizationChecker.OrgCodeIsUniqueAsync(code))
                throw new BusinessException("组织机构编码已存在");

            //获取Id及Id-Path
            var id = _numberIdGenerator.GenerateUniqueId();
            string nodePath = await base.GetNodePath(parentId, id, 5);

            //返回
            return new Organization(code)
            {
                Id = id,
                Name = name,
                OrgTypeCode = orgTypeCode,
                AreaCode = areaCode,
                ParentId = parentId,
                NodePath = nodePath,
                State = EntityStates.Normal
            };
        }

        protected override void ChangeTreeNodePath(Organization entity, string nodePath)
        {
            entity.NodePath = nodePath;
        }
    }
}
