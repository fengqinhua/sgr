/**************************************************************
 * 
 * 唯一标识：87a4d59e-146e-437f-9e56-11155d70d03c
 * 命名空间：Sgr.Domain.Entities.Auditing
 * 创建时间：2023/7/26 12:22:02
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Domain.Entities.Auditing
{
    /// <summary>
    /// 审核操作接口
    /// </summary>
    public interface IAuditedOperator
    {
        /// <summary>
        /// 更新实体创建相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        void UpdateCreationAudited(object targetObject);
        /// <summary>
        /// 更新实体修改相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        void UpdateModifyAudited(object targetObject);
        /// <summary>
        /// 更新实体删除相关审计信息
        /// </summary>
        /// <param name="targetObject"></param>
        void UpdateDeleteAudited(object targetObject);

    }
}
