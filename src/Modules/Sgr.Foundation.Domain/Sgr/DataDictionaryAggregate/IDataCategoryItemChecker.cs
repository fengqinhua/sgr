using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Sgr.DataDictionaryAggregate
{
    public interface IDataCategoryItemChecker
    {
        /// <summary>
        /// 编码是否唯一
        /// </summary>
        /// <param name="value"></param>
        /// <param name="categoryTypeCode"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ValueIsUniqueAsync(string value,
            string categoryTypeCode,
            long id = 0,
            CancellationToken cancellationToken = default);
    }
}
