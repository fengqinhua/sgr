using Sgr.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.AuditLogAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogOperateRepository : IBaseRepositoryOfTEntityAndTPrimaryKey<LogOperate, long>
    {
    }
}
