using Sgr.Domain.Entities;
using Sgr.Domain.Managers;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Domain.Duties
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDutyManage : IDomainManager
    {
        /// <summary>
        /// 创建职务
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="orderNumber"></param>
        /// <param name="remarks"></param>
        /// <param name="state"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<Duty> CreateNewAsync(
            string code,
            string name,
            int orderNumber,
            string? remarks,
            EntityStates state,
            long orgId = 0);

        /// <summary>
        /// 检查职务编码是否符合业务要求
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <param name="orgId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BusinessCheckResult> IsUniqueAsync(
            string code,
            long id = 0,
            long orgId = 0,
            CancellationToken cancellationToken = default);


    }
}
