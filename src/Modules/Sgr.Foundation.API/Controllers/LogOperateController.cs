using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sgr.Application.ViewModels;
using Sgr.AspNetCore.ActionFilters;
using Sgr.AuditLogAggregate;
using Sgr.Foundation.API.Application.Queries.AuditLog;
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
        private readonly IMediator _mediator;
        private readonly ILogOperateQueries _logOperateQueries;

        public LogOperateController(IMediator mediator, ILogOperateQueries logOperateQueries)
        {
            _mediator = mediator;
            _logOperateQueries = logOperateQueries;
        }

        /// <summary>
        /// 查询审计日志列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("list")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AuditLogActionFilter("查询审计日志列表")]
        public async Task<ActionResult<PagedResponse<OutLogOperatePaged>>> GetListAsync(InLogOperatePagedRequest request)
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
        public async Task<ActionResult<OutLogOperate>> GetAsync(long id)
        {
            //权限认证

            if (id < 0)
                return NotFound();

            var result = await _logOperateQueries.GetAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


    }
}
