﻿using Microsoft.EntityFrameworkCore;
using Sgr.AuditLogAggregate;
using Sgr.Domain.Entities.Auditing;
using Sgr.Domain.Uow;
using Sgr.EntityFrameworkCore;
using Sgr.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Domain.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class LogOperateRepository
        : EfCoreRepositoryOfTEntityAndTPrimaryKey<LogOperate, long>, ILogOperateRepository
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
        public LogOperateRepository(SgrDbContext context, IAuditedOperator auditedOperator, INumberIdGenerator numberIdGenerator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _auditedOperator = auditedOperator ?? throw new ArgumentNullException(nameof(auditedOperator));
            _numberIdGenerator = numberIdGenerator ?? throw new ArgumentNullException(nameof(numberIdGenerator));
        }

        /// <summary>
        /// 设置主键Id
        /// </summary>
        /// <param name="entity"></param>
        protected override void CheckAndSetId(LogOperate entity)
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