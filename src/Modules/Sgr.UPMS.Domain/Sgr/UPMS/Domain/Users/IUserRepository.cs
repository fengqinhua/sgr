using Sgr.Domain.Repositories;
using System.Threading.Tasks;
using System.Threading;

namespace Sgr.UPMS.Domain.Users
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository : IBaseRepositoryOfTEntityAndTPrimaryKey<User, long>
    {
        Task<User?> GetByLoginNameAsync(string loginName,
            CancellationToken cancellationToken = default);

        Task<User?> GetByLoginNameAsync(string loginName,
            string[] propertiesToInclude,
            CancellationToken cancellationToken = default);
    }
}
