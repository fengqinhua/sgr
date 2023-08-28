/**************************************************************
 * 
 * 唯一标识：40cd6bb5-c032-4219-ae06-9f27f2fe5f9a
 * 命名空间：Sgr.UPMS.Infrastructure.Repositories
 * 创建时间：2023/8/28 9:32:32
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
using Sgr.Exceptions;
using Sgr.Generator;
using Sgr.UPMS.Domain.Users;
using Sgr.UPMS.Domain.UserTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Infrastructure.Repositories
{
    public class UserRefreshTokenRepository : EfCoreRepositoryOfTEntityAndTPrimaryKey<UserRefreshToken, long>, IUserRefreshTokenRepository
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
        public UserRefreshTokenRepository(SgrDbContext context, IAuditedOperator auditedOperator, INumberIdGenerator numberIdGenerator)
        {
            _context = context ?? throw new BusinessException("SgrDbContext Is Null");
            _auditedOperator = auditedOperator ?? throw new BusinessException("IAuditedOperator Is Null");
            _numberIdGenerator = numberIdGenerator ?? throw new BusinessException("INumberIdGenerator Is Null");
        }

        /// <summary>
        /// 设置主键Id
        /// </summary>
        /// <param name="entity"></param>
        protected override void CheckAndSetId(UserRefreshToken entity)
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

        public async Task<UserRefreshToken?> GetByRefreshTokenAsync(string refreshToken,
               CancellationToken cancellationToken = default)
        {
            return await GetQueryable().FirstOrDefaultAsync(f => f.Token == refreshToken, cancellationToken);
        }
    }
}
