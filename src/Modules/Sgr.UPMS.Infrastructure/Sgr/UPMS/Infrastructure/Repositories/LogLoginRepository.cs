using Microsoft.EntityFrameworkCore;
using Sgr.Domain.Entities.Auditing;
using Sgr.Domain.Repositories;
using Sgr.Domain.Uow;
using Sgr.EntityFrameworkCore;
using Sgr.Exceptions;
using Sgr.Generator;
using Sgr.UPMS.Domain.LogLogins;
using System;

namespace Sgr.UPMS.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class LogLoginRepository
        : EfCoreRepositoryOfTEntityAndTPrimaryKey<LogLogin, long>, ILogLoginRepository
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
        public LogLoginRepository(SgrDbContext context, IAuditedOperator auditedOperator, INumberIdGenerator numberIdGenerator)
        {
            _context = context ?? throw new BusinessException("SgrDbContext Is Null");
            _auditedOperator = auditedOperator ?? throw new BusinessException("IAuditedOperator Is Null");
            _numberIdGenerator = numberIdGenerator ?? throw new BusinessException("INumberIdGenerator Is Null");
        }

        /// <summary>
        /// 设置主键Id
        /// </summary>
        /// <param name="entity"></param>
        protected override void CheckAndSetId(LogLogin entity)
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
