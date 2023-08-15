using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Services
{
    public interface ICategoryTypeProvider
    {
        IList<NameValue> GetCategoryTypes();
    }
}
