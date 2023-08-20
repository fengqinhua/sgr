using Sgr.Domain.Repositories;
using System.Threading.Tasks;
using System.Threading;

namespace Sgr.UPMS.Domain.Duties
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDutyRepository : IBaseRepositoryOfTEntityAndTPrimaryKey<Duty, long>
    {
        /// <summary>
        /// 机构中是否已存在指定的岗位编码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="exclude_dutyId"></param>
        /// <param name="orgId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> ExistAsync(string code, long exclude_dutyId = 0, long orgId = 0, CancellationToken cancellationToken = default);
    }
}
