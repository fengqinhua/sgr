/**************************************************************
 * 
 * 唯一标识：55058058-c40f-4eb0-88fc-810750fc8194
 * 命名空间：Sgr.DataCategories
 * 创建时间：2023/8/20 16:44:45
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Application.Services;
using System.Collections.Generic;

namespace Sgr.DataCategories
{
    public class CategoryTypeProvider : ICategoryTypeProvider
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
