using Sgr.Domain.Repositories;
using Sgr.OrganizationAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Sgr.DepartmentAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDepartmentRepository : ITreeNodeBaseRepositoryOfTEntityAndTPrimaryKey<Department, long>
    {

    }
}