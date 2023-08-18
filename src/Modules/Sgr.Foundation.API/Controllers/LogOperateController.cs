﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sgr.Application.ViewModels;
using Sgr.AspNetCore.ActionFilters;
using Sgr.AuditLogAggregate;
using Sgr.Foundation.API.Application.AuditLog.Queries;
using Sgr.Foundation.API.Application.AuditLog.ViewModels;
using System.Threading.Tasks;

namespace Sgr.Foundation.API.Controllers
{
    /// <summary>
    /// 审计日志
    /// </summary>
    //[Authorize]
    [Route("api/v1/sgr/[controller]")]
    [ApiController]
    public class LogOperateController : ControllerBase
    {
        private readonly ILogOperateQueries _logOperateQueries;

        public LogOperateController(ILogOperateQueries logOperateQueries)
        {
            _logOperateQueries = logOperateQueries;
        }



        #region Queries
        /// <summary>
        /// 查询审计日志列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("list")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AuditLogActionFilter("查询审计日志列表")]
        public async Task<ActionResult<PagedResponse<LogOperateSearchModel>>> GetListAsync(LogOperateListModel request)
        {
            //权限认证

            var result = await _logOperateQueries.GetListAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// 查询审计日志详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        [Route("details/{id:long}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuditLogActionFilter("查询审计日志详情")]
        public async Task<ActionResult<LogOperateModel>> GetAsync(long id)
        {
            //权限认证

            if (id < 0)
                return NotFound();

            var result = await _logOperateQueries.GetAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        #endregion
    }
}
