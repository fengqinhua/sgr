﻿using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Domain.Organizations
{
    public interface IOrganizationChecker
    {
        /// <summary>
        /// 组织编码是否唯一
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> OrgCodeIsUniqueAsync(string code,
            long id = 0,
            CancellationToken cancellationToken = default);
    }
}
