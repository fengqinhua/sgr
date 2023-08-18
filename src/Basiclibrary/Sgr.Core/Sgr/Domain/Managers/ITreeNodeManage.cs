using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Sgr.Domain.Entities;

namespace Sgr.Domain.Managers
{
    public interface ITreeNodeManage<TEntity, TPrimaryKey> : IDomainManager
        where TEntity : class, IEntity<TPrimaryKey>, ITreeNode<TPrimaryKey>, IAggregateRoot
    {
        Task<bool> RemoveNodeAsync(TPrimaryKey id,
            CancellationToken cancellationToken = default);

        Task<string> GetNodePathAsync(TPrimaryKey parentId, TPrimaryKey thisId, int maxLevel = 5, CancellationToken cancellationToken = default);

        Task ChangeParentIdAsync(
            TEntity entity,
            TPrimaryKey newParentId,
            int maxLevel = 5,
            CancellationToken cancellationToken = default);
    }
}
