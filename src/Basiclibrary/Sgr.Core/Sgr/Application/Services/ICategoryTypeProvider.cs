using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Application.Services
{
    public interface ICategoryTypeProvider
    {
        IList<NameValue> GetCategoryTypes();
    }
}
