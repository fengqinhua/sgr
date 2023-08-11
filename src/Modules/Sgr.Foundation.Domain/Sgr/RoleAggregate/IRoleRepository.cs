using Sgr.Domain.Repositories;
using Sgr.DutyAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.RoleAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoleRepository : IBaseRepositoryOfTEntityAndTPrimaryKey<Role, long>
    {
    }
}
