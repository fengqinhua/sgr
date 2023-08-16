using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Application.ViewModels
{
    [Serializable]
    public class ListResponse<T>
    {
        private IReadOnlyList<T>? _items;
        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }

        public ListResponse(IReadOnlyList<T>? items)
        {
            _items = items;
        }
    }
}
