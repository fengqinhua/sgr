using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Application.ViewModels
{
    [Serializable]
    public class ListResponse<TViewModel>
    {
        private IReadOnlyList<TViewModel>? _items;
        public IReadOnlyList<TViewModel> Items
        {
            get { return _items ??= new List<TViewModel>(); }
            set { _items = value; }
        }

        public ListResponse(IReadOnlyList<TViewModel>? items)
        {
            _items = items;
        }
    }
}
