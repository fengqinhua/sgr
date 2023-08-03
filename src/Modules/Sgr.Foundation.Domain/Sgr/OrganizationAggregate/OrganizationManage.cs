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
using Sgr.Exceptions;
using Sgr.Generator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.OrganizationAggregate
{
    /// <summary>
    /// 组织编码检查
    /// </summary>
    public class OrganizationManage : IOrganizationManage
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly INumberIdGenerator _numberIdGenerator;

        /// <summary>
        /// 组织编码检查
        /// </summary>
        /// <param name="organizationRepository"></param>
        /// <param name="numberIdGenerator"></param>
        public OrganizationManage(IOrganizationRepository organizationRepository,
            INumberIdGenerator numberIdGenerator) 
        {
            _organizationRepository = organizationRepository;
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
            (await IsUniqueAsync(code)).HandleRule();

            //获取Id及Id-Path
            var id = _numberIdGenerator.GenerateUniqueId();
            string nodePath = await getNodePath(parentId, id);

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



        /// <summary>
        /// 调整组织机构的父节点
        /// </summary>
        /// <param name="org"></param>
        /// <param name="newParentId"></param>
        /// <returns></returns>
        public async Task ChangeParentIdAsync(Organization org, long newParentId)
        {
            string oldNodePath = org.NodePath;
            string newNodePath = await getNodePath(newParentId, org.Id);

            var orgs = await _organizationRepository.GetChildNodesRecursionAsync(org);

            foreach(var item in orgs)
            {
                item.NodePath = item.NodePath.Replace(oldNodePath, newNodePath);
            }

            org.ParentId = newParentId;
        }


        /// <summary>
        /// 是否符合组织编码规范要求
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public  async Task<BusinessCheckResult> IsUniqueAsync(string code, long id = 0)
        {
            if (await _organizationRepository.ExistAsync(code, id))
                return BusinessCheckResult.Fail("组织机构编码已存在");

            return BusinessCheckResult.Ok();
        }


        private async Task<string> getNodePath(long parentId, long thisId)
        {
            string nodePath;
            if (parentId > 0)
            {
                var org = (await _organizationRepository.GetAsync(parentId)) ?? throw new BusinessException($"上级组织(Id：{parentId})不存在");

                if (org.NodePath.Split('#').Length >= 5)
                    throw new BusinessException($"组织机构目录层级不允许超过5层！");

                nodePath = $"{org.NodePath}#{thisId}";
            }
            else
                nodePath = $"{thisId}";
            return nodePath;
        }
    }
}
