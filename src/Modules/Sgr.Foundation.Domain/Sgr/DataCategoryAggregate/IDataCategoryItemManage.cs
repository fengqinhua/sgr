using Sgr.Domain.Managers;
using Sgr.OrganizationAggregate;
using System.Threading.Tasks;

namespace Sgr.DataCategoryAggregate
{
    public interface IDataCategoryItemManage : ITreeNodeManage<DataCategoryItem, long>
    {

        Task<DataCategoryItem> CreateNewAsync(
            string name,
            string value,
            string? remarks,
            string categoryTypeCode,
            int orderNumber,
            long parentId = 0);
    }
}
