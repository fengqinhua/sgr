﻿using Microsoft.EntityFrameworkCore;
using Sgr.EntityFrameworkCore;
using Sgr.OrganizationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.Domain.Checkers
{
    public class OrganizationChecker: IOrganizationChecker
    {
        private readonly SgrDbContext _context;

        public OrganizationChecker(SgrDbContext context)
        {
            _context = context;
        }

        public Task<bool> OrgCodeIsUniqueAsync(string code, long id = 0, CancellationToken cancellationToken = default)
        {
            return _context.Set<Organization>().AnyAsync(f=>f.Code == code && f.Id != id, cancellationToken);
        }
    }
}
