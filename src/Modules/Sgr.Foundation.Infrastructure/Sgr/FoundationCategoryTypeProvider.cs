using Sgr.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr
{
    public class FoundationCategoryTypeProvider : ICategoryTypeProvider
    {
        public const string CategoryType_OrgType_Code = "Foundation.OrgType";

        public IList<NameValue> GetCategoryTypes()
        {
            return new List<NameValue>()
            {
                new NameValue("机构类型",CategoryType_OrgType_Code)
            };
        }
    }
}
