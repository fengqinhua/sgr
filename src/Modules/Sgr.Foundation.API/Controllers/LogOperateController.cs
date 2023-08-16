using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sgr.AspNetCore.ActionFilters;
using Sgr.AuditLogAggregate;
using System.Threading.Tasks;

namespace Sgr.Foundation.API.Controllers
{
    /// <summary>
    /// 审计日志
    /// </summary>
    //[Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LogOperateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogOperateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 查询审计日志详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        [Route("{id:long}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuditLogActionFilter("查询审计日志详情")]
        public async Task<ActionResult<LogOperate>> LogOperateAsync(int id)
        {
            return new LogOperate();
        }

    }
}
