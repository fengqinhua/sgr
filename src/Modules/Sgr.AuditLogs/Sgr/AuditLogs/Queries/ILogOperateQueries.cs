﻿/**************************************************************
 * 
 * 唯一标识：617da7a4-cb20-4d0c-a760-123c372d1f4f
 * 命名空间：Sgr.AuditLogs.Queries
 * 创建时间：2023/8/16 16:51:26
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Application.ViewModels;
using Sgr.AuditLogs.ViewModels;
using System.Threading.Tasks;

namespace Sgr.AuditLogs.Queries
{
    public interface ILogOperateQueries
    {
        /// <summary>
        /// 查询审计日志详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LogOperateModel?> GetAsync(long id);

        /// <summary>
        /// 查询审计日志列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResponse<LogOperateSearchModel>> GetListAsync(LogOperateListModel request);

    }
}
