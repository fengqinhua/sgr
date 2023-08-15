using Microsoft.Extensions.Logging;
using Sgr.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.Database
{
    public class DefaultDatabaseSeed : IDatabaseSeed
    {
        private readonly ILogger<DefaultDatabaseSeed> _logger;
        private readonly IEnumerable<IDataBaseInitialize> _initializes;

        public DefaultDatabaseSeed(ILogger<DefaultDatabaseSeed> logger, IEnumerable<IDataBaseInitialize> initializes)
        {
            _logger = logger;
            _initializes = initializes;
        }

        public async Task SeedAsync()
        {
            if(!_initializes.Any())
            {
                _logger.LogInformation($"No DataBase Initialize Set!");
                return;
            }

            List<IDataBaseInitialize> items = _initializes.OrderBy(f => f.Order).ToList();

            foreach (IDataBaseInitialize item in items)
            {
                await item.Initialize();

                _logger.LogInformation($"Item [{item.Order}] Initialize Task Complete ...");
            }

            _logger.LogInformation($"Database Seed Complete! Total {items.Count} items");
        }
    }
}
