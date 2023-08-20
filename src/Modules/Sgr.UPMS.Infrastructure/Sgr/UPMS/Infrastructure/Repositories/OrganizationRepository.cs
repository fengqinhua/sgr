/**************************************************************
 * 
 * 唯一标识：1f0dc49b-3482-43b3-9cdc-bf891df93e2e
 * 命名空间：Sgr.Domain.Repositories
 * 创建时间：2023/8/4 11:33:15
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Sgr.Domain.Entities.Auditing;
using Sgr.Domain.Repositories;
using Sgr.Domain.Uow;
using Sgr.EntityFrameworkCore;
using Sgr.Generator;
using Sgr.UPMS.Domain.Organizations;
using System;

namespace Sgr.UPMS.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class OrganizationRepository :
        EfCoreTreeNodeBaseRepositoryOfTEntityAndTPrimaryKey<Organization, long>, IOrganizationRepository
    {

        private readonly SgrDbContext _context;
        private readonly IAuditedOperator _auditedOperator;
        private readonly INumberIdGenerator _numberIdGenerator;
        /// <summary>
        /// 
        /// </summary>
        public override IUnitOfWork UnitOfWork => _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="auditedOperator"></param>
        /// <param name="numberIdGenerator"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public OrganizationRepository(SgrDbContext context, IAuditedOperator auditedOperator, INumberIdGenerator numberIdGenerator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _auditedOperator = auditedOperator ?? throw new ArgumentNullException(nameof(auditedOperator));
            _numberIdGenerator = numberIdGenerator ?? throw new ArgumentNullException(nameof(numberIdGenerator));
        }

        /// <summary>
        /// 设置主键Id
        /// </summary>
        /// <param name="entity"></param>
        protected override void CheckAndSetId(Organization entity)
        {
            if (entity.Id == 0)
                entity.Id = _numberIdGenerator.GenerateUniqueId();
        }

        /// <summary>
        /// 设置审计接口
        /// </summary>
        /// <returns></returns>
        protected override IAuditedOperator? GetAuditedOperator()
        {
            return _auditedOperator;
        }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        protected override DbContext GetDbContext()
        {
            return _context;
        }
    }

}
