using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.DataCategories.Services
{
    public interface ICategoryTypeProvider
    {
        IList<NameValue> GetCategoryTypes();
    }
}
