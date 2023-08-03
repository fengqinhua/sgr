/**************************************************************
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
    /// 审核操作接口的缺省实现
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    /// <typeparam name="TOrgKey"></typeparam>
    public class DefaultAuditedOperator<TUserKey, TOrgKey> : IAuditedOperator
    {
        private readonly ICurrentUser<TUserKey, TOrgKey> _currentUser;

        /// <summary>
        /// 审核操作接口的缺省实现
        /// </summary>
        /// <param name="currentUser"></param>
        public DefaultAuditedOperator(ICurrentUser<TUserKey, TOrgKey> currentUser)
        {
            _currentUser = currentUser;
        }

        /// <summary>
        /// 更新实体创建相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        public void UpdateCreationAudited(object targetObject)
        {
            if (targetObject is ICreationAudited<TUserKey> creationAuditedEntity)
            {
                if (creationAuditedEntity.CreatorUserId == null)
                    creationAuditedEntity.CreatorUserId = _currentUser.Id;

                if (creationAuditedEntity.CreationTime == default)
                    creationAuditedEntity.CreationTime = DateTimeOffset.Now;
            }
        }

        /// <summary>
        /// 更新实体修改相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        public void UpdateModifyAudited(object targetObject)
        {
            if (targetObject is ICreationAndModifyAudited<TUserKey> creationAndModifyAuditedEntity)
            {
                creationAndModifyAuditedEntity.LastModificationTime = DateTimeOffset.Now;
                creationAndModifyAuditedEntity.LastModifierUserId = _currentUser.Id;
            }

        }
        /// <summary>
        /// 更新实体删除相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        public void UpdateDeleteAudited(object targetObject)
        {
            if (targetObject is IFullAudited<TUserKey> fullAuditedEntity)
            {
                fullAuditedEntity.DeletionTime = DateTimeOffset.Now;
                fullAuditedEntity.DeleterUserId = _currentUser.Id;
                fullAuditedEntity.IsDeleted = true;
            }
        }
    }
}
