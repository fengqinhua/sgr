using Sgr.Domain.Entities;
using Sgr.Domain.Repositories;
using Sgr.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.Domain.Managers
{
    public abstract class TreeNodeManageBase<TEntity, TPrimaryKey> : ITreeNodeManage<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, ITreeNode<TPrimaryKey>, IAggregateRoot
    {
        private readonly ITreeNodeBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> _repository;

        public TreeNodeManageBase(ITreeNodeBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> repository)
        {
            _repository = repository;
        }

        public async Task ChangeParentIdAsync(
            TEntity entity,
            TPrimaryKey newParentId,
            int maxLevel = 5,
            CancellationToken cancellationToken = default)
        {
            string oldNodePath = entity.NodePath;
            string newNodePath = await GetNodePath(newParentId, entity.Id, maxLevel, cancellationToken);

            var sons = await _repository.GetChildNodesRecursionAsync(entity, cancellationToken);

            foreach (var item in sons)
            {
                ChangeTreeNodePath(item, item.NodePath.Replace(oldNodePath, newNodePath));
                await _repository.UpdateAsync(item);
            }
        }


        protected abstract void ChangeTreeNodePath(TEntity entity, string nodePath);

        public async Task<string> GetNodePath(TPrimaryKey parentId, TPrimaryKey thisId,int maxLevel = 5, CancellationToken cancellationToken = default)
        {
            string nodePath;
            if (parentId!.Equals(default(TPrimaryKey)))
            {
                var parentNode = (await _repository.GetAsync(parentId, cancellationToken)) ?? throw new BusinessException($"上级节点(Id：{parentId})不存在");

                if (parentNode.NodePath.Split('#').Length >= maxLevel)
                    throw new BusinessException($"层级不允许超过{maxLevel}层！");

                nodePath = $"{parentNode.NodePath}#{thisId}";
            }
            else
                nodePath = $"{thisId}";

            return nodePath;
        }


    }
}
