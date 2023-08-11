using Sgr.Domain.Repositories;
using Sgr.RoleAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.UserAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository : IBaseRepositoryOfTEntityAndTPrimaryKey<User, long>
    {
    }
}
