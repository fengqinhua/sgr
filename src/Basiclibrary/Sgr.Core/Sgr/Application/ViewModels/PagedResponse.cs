using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Application.ViewModels
{
    [Serializable]
    public class PagedResponse<T> : ListResponse<T>
    {
        public long Total { get; set; }

        public PagedResponse()
            : this(0, null)
        {

        }

        public PagedResponse(long totalCount, IReadOnlyList<T>? items)
            : base(items)
        {
            this.Total = totalCount;
        }

    }
}
