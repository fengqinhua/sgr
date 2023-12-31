﻿/**************************************************************
 * 
 * 唯一标识：1519d87d-57ec-49e3-83ba-2bc1c04b2c7e
 * 命名空间：Sgr.Domain.Entities.Auditing
 * 创建时间：2023/7/26 12:24:59
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Sgr.Domain.Entities.Auditing
{
    /// <summary>
    /// 审核操作接口的缺省实现(主键使用Long)
    /// </summary>
    public class DefaultAuditedOperator : IAuditedOperator
    {
        private readonly ICurrentUser _currentUser;

        /// <summary>
        /// 审核操作接口的缺省实现
        /// </summary>
        /// <param name="currentUser"></param>
        public DefaultAuditedOperator(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        /// <summary>
        /// 更新实体创建相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        public virtual void UpdateCreationAudited(object targetObject)
        {
            if (targetObject is ICreationAudited<long> creationAuditedEntity)
            {
                if (creationAuditedEntity.CreatorUserId == 0)
                {
                    if (!long.TryParse(_currentUser.Id, out long userId))
                        userId = Constant.DEFAULT_USERID;

                    creationAuditedEntity.CreatorUserId = userId;
                }

                if (creationAuditedEntity.CreationTime == default)
                    creationAuditedEntity.CreationTime = DateTimeOffset.UtcNow;
            }
        }

        /// <summary>
        /// 更新实体修改相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        public virtual void UpdateModifyAudited(object targetObject)
        {
            if (targetObject is ICreationAndModifyAudited<long> creationAndModifyAuditedEntity)
            {
                creationAndModifyAuditedEntity.LastModificationTime = DateTimeOffset.UtcNow;

                if (long.TryParse(_currentUser.Id, out long userId))
                    userId = Constant.DEFAULT_USERID;

                creationAndModifyAuditedEntity.LastModifierUserId = userId;
            }

            if (targetObject is IOptimisticLock optimisticLock)
                optimisticLock.RowVersion += 1;
        }
        /// <summary>
        /// 更新实体删除相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        public virtual void UpdateDeleteAudited(object targetObject)
        {
            if (targetObject is IFullAudited<long> fullAuditedEntity)
            {
                fullAuditedEntity.DeletionTime = DateTimeOffset.UtcNow;
                fullAuditedEntity.IsDeleted = true;

                if (long.TryParse(_currentUser.Id, out long userId))
                    userId = Constant.DEFAULT_USERID;
                fullAuditedEntity.DeleterUserId = userId;
            }


        }
    }
}
