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
using Sgr.UPMS.Domain.Users;
using System.Threading.Tasks;

namespace Sgr.UPMS.Domain.Organizations
{
    /// <summary>
    /// 组织编码检查
    /// </summary>
    public class OrganizationManage : TreeNodeManageBase<Organization, long>, IOrganizationManage
    {
        private readonly INumberIdGenerator _numberIdGenerator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizationRepository"></param>
        /// <param name="numberIdGenerator"></param>
        public OrganizationManage(IOrganizationRepository organizationRepository,
            INumberIdGenerator numberIdGenerator)
            : base(organizationRepository)
        {
            _numberIdGenerator = numberIdGenerator;
        }



        /// <summary>
        /// 创建一个新的组织机构
        /// </summary>
        /// <param name="name"></param>
        /// <param name="orgTypeCode"></param>
        /// <param name="areaCode"></param>
        /// <param name="staffSizeCode"></param>
        /// <param name="leader"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="orderNumber"></param>
        /// <param name="remarks"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<Organization> CreateNewAsync(string name,
            string orgTypeCode,
            string areaCode,
            string staffSizeCode,
            string? leader,
            string? phone,
            string? email,
            string? address,
            int orderNumber,
            string? remarks,
            long? parentId)
        {
            Check.StringNotNullOrWhiteSpace(name, nameof(name));
            Check.StringNotNullOrWhiteSpace(orgTypeCode, nameof(orgTypeCode));
            Check.StringNotNullOrWhiteSpace(areaCode, nameof(areaCode));

            //获取Id及Id-Path
            long id = _numberIdGenerator.GenerateUniqueId();
            string nodePath = "";
            if (parentId.HasValue)
                nodePath = await GetNodePathAsync(parentId.Value, id, 5);

            return new Organization(name, orgTypeCode, areaCode, staffSizeCode, leader, phone, email, address, orderNumber, remarks)
            {
                Id = id,
                ParentId = parentId ?? 0,
                NodePath = nodePath
            };
        }

        /// <summary>
        /// 组织注销前审查
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> CancellationExamination(Organization organization, User user)
        {
            if (!user.IsSuperAdmin && organization.Id != user.OrgId)
                throw new BusinessException("注销组织必须由当前组织的用户执行!");

            //如果当前组织有下级组织，则不允许注销

            if (await this._repository.HasChildNodesAsync(organization))
                return false;

            return true;
        }


        protected override void ChangeTreeNodePath(Organization entity, long parentId, string nodePath)
        {
            entity.ParentId = parentId;
            entity.NodePath = nodePath;
        }
    }
}
