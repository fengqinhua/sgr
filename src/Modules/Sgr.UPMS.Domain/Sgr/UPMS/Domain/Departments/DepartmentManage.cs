/**************************************************************
 * 
 * 唯一标识：c70e01a0-b152-47ed-be07-35b6ea5a6c17
 * 命名空间：Sgr.UPMS.Domain.Departments
 * 创建时间：2023/8/27 19:42:32
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Managers;
using Sgr.Domain.Repositories;
using Sgr.Generator;
using Sgr.UPMS.Domain.Organizations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.UPMS.Domain.Departments
{
    public class DepartmentManage : TreeNodeManageBase<Department, long>, IDepartmentManage
    {
        private readonly INumberIdGenerator _numberIdGenerator;

        public DepartmentManage(IDepartmentRepository repository,
            INumberIdGenerator numberIdGenerator) : base(repository)
        {
            _numberIdGenerator = numberIdGenerator;
        }

        public async Task<Department> CreateNewAsync(string name, int orderNumber, string? remarks, string? leader, string? phone, string? email, long orgId, long? parentId)
        {
            Check.StringNotNullOrWhiteSpace(name, nameof(name));

            //获取Id及Id-Path
            long id = _numberIdGenerator.GenerateUniqueId();
            string nodePath = "";
            if (parentId.HasValue)
                nodePath = await GetNodePathAsync(parentId.Value, id, 5);

            return new Department(Guid.NewGuid().ToString("N"), name, orderNumber, remarks, orgId)
            {
                Id = id,
                ParentId = parentId ?? 0,
                NodePath = nodePath
            };
        }

        protected override void ChangeTreeNodePath(Department entity, long parentId, string nodePath)
        {
            entity.ParentId = parentId;
            entity.NodePath = nodePath;
        }
    }
}
