using Sgr.Domain.Managers;
using Sgr.OrganizationAggregate;
using System.Threading.Tasks;

namespace Sgr.DataDictionaryAggregate
{
    public interface IDataCategoryItemManage : ITreeNodeManage<DataCategoryItem, long>
    {
       
        Task<DataCategoryItem> CreateNewAsync(
            string name,
            string value,
            string categoryTypeCode,
            int orderNumber,
            long parentId = 0);
    }
}
