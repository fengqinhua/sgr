using Microsoft.EntityFrameworkCore;
using Sgr.DataDictionaryAggregate;
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
    public class DataCategoryItemChecker : IDataCategoryItemChecker
    {
        private readonly SgrDbContext _context;

        public DataCategoryItemChecker(SgrDbContext context)
        {
            _context = context;
        }

        public Task<bool> ValueIsUniqueAsync(string value,
            string categoryTypeCode, 
            long id = 0, 
            CancellationToken cancellationToken = default)
        {
            return _context.Set<DataCategoryItem>().AnyAsync(f => f.DcItemValue == value &&f.CategoryTypeCode == categoryTypeCode && f.Id != id, cancellationToken);
        }
    }
}
