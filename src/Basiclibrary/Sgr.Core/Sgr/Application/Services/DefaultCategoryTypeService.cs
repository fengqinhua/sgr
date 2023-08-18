using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.Application.Services
{
    public class DefaultCategoryTypeService : ICategoryTypeService
    {
        private static readonly object _initLock = new();
        private List<NameValue>? _datas = null;

        private readonly IEnumerable<ICategoryTypeProvider> _providers;

        public DefaultCategoryTypeService(IEnumerable<ICategoryTypeProvider> providers)
        {
            _providers = providers;
        }

        public Task<NameValue[]> GetAllAsync()
        {
            EnsureInitialized();
            return Task.FromResult(_datas!.ToArray());
        }

        public async Task<NameValue?> GetAsync(string value)
        {
            NameValue? result = null;
            var item = (await GetAllAsync()).FirstOrDefault(f => f.Value == value);
            if (item != null)
                result = new NameValue(item!.Name, item!.Value);
            return result;
        }

        private void EnsureInitialized()
        {
            if (_datas == null)
            {
                lock (_initLock)
                {
                    _datas ??= GetList();
                }
            }
        }


        public List<NameValue> GetList()
        {
            List<NameValue> result = new();

            foreach (var item in _providers)
            {
                var categoryTypes = item.GetCategoryTypes();
                foreach (var categoryType in categoryTypes)
                {
                    if (!result.Any(f => f.Value == categoryType.Value))
                        result.Add(categoryType);
                }
            }

            return result.OrderBy(f => f.Value).ToList();
        }
    }
}
