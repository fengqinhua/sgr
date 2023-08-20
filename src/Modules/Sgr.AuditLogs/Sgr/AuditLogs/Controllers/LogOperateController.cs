/**************************************************************
 * 
 * 唯一标识：ab46d708-7df5-4fbc-80c4-aaf86719f6d7
 * 命名空间：Sgr.AuditLogs.Controllers
 * 创建时间：2023/8/20 15:58:24
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sgr.Application.ViewModels;
using Sgr.AspNetCore.ActionFilters;
using Sgr.AuditLogs.Queries;
using Sgr.AuditLogs.ViewModels;
using System.Threading.Tasks;

namespace Sgr.AuditLogs.Controllers
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
