using Sgr.Application.Services;
using System.Collections.Generic;

namespace Sgr
{
    public class FoundationCategoryTypeProvider : ICategoryTypeProvider
    {
        public const string CategoryType_Area_Code = "F_AREA";
        public const string CategoryType_OrgType_Code = "F_ORGTYPE";

        public const string CategoryType_SoftClassification_Code = "F_SOFT_CLASSIFICATION";
        public const string CategoryType_SoftType_Code = "F_SOFT_TYPE";

        public IList<NameValue> GetCategoryTypes()
        {
            return new List<NameValue>()
            {
                new NameValue("行政区划",CategoryType_Area_Code),
                new NameValue("机构类型",CategoryType_OrgType_Code),
                new NameValue("软件分类",CategoryType_SoftClassification_Code),
                new NameValue("软件类型",CategoryType_SoftType_Code)
            };
        }
    }
}
