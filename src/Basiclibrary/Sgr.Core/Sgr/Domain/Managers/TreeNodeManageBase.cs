﻿using Sgr.Domain.Entities;
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
        protected readonly ITreeNodeBaseRepositoryOfTEntityAndTPrimaryKey<TEntity, TPrimaryKey> _repository;

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
            string newNodePath = await GetNodePathAsync(newParentId, entity.Id, maxLevel, cancellationToken);

            //先更新当前节点
            ChangeTreeNodePath(entity, newParentId, newNodePath);
            await _repository.UpdateAsync(entity);

            //再更新子孙节点
            var sons = await _repository.GetChildNodesRecursionAsync(entity, cancellationToken);
            foreach (var item in sons)
            {
                ChangeTreeNodePath(item, item.ParentId, item.NodePath.Replace(oldNodePath, newNodePath));

                await _repository.UpdateAsync(item);
            }
        }


        protected abstract void ChangeTreeNodePath(TEntity entity, TPrimaryKey parentId,string nodePath);
         
        public async Task<string> GetNodePathAsync(TPrimaryKey parentId, TPrimaryKey thisId,int maxLevel = 5, CancellationToken cancellationToken = default)
        {
            string nodePath;
            if (parentId!.Equals(default(TPrimaryKey)))
                nodePath = $"0";
            else
            {
                var parentNode = (await _repository.GetAsync(parentId, cancellationToken));// ?? throw new BusinessException($"上级节点(Id：{parentId})不存在");
                if (parentNode == null)
                    nodePath = $"0";
                else
                {
                    if (parentNode.NodePath.Split('#').Length >= maxLevel)
                        throw new BusinessException($"层级不允许超过{maxLevel}层！");
                    nodePath = $"{parentNode.NodePath}#{thisId}";
                }
            }

            return nodePath;
        }

        public async Task<bool> RemoveNodeAsync(TPrimaryKey id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(id, cancellationToken);
            if (entity == null)
                return false;

            var sons = await _repository.GetChildNodesRecursionAsync(entity!, cancellationToken);

            foreach(var son in sons )
            {
                await _repository.DeleteAsync(son);
            }

            await _repository.DeleteAsync(entity);

            return await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
