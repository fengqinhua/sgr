using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Application.ViewModels
{
    [Serializable]
    public class PagedResponse<TViewModel> : ListResponse<TViewModel>
    {
        public long Total { get; set; }

        public PagedResponse()
            : this(0, null)
        {

        }

        public PagedResponse(long totalCount, IReadOnlyList<TViewModel>? items)
            : base(items)
        {
            this.Total = totalCount;
        }

    }
}
