/**************************************************************
 * 
 * 唯一标识：56967dd2-3e85-456d-b0e8-5986d4747289
 * 命名空间：Sgr.UPMS.Controllers
 * 创建时间：2023/8/27 19:32:14
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sgr.AspNetCore.ActionFilters.AuditLogs;
using Sgr.ExceptionHandling;
using Sgr.UPMS.Application.Commands.Duties;
using System.Threading.Tasks;

namespace Sgr.UPMS.Controllers
{
    /// <summary>
    /// 职务管理
    /// </summary>
    [Route("api/v1/sgr/[controller]")]
    [ApiController]
    public class DutyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthorizationService _authorizationService;

        public DutyController(IMediator mediator,
            IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _authorizationService = authorizationService;
        }

        #region Command

        /// <summary>
        /// 创建职务
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("创建职务")]
        public async Task<ActionResult<bool>> CreateAsync([FromBody] CreateDutyCommand command)
        {
            if (await _authorizationService.AuthorizeAsync(User, Permissions.CreateDutyPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 修改职务
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("修改职务")]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] UpdateDutyCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.UpdateDutyPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 删除职务
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("删除职务")]
        public async Task<ActionResult<bool>> DeleteAsync([FromBody] DeleteDutyCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.DeleteDutyPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 调整职务状态
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("change-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("调整职务状态")]
        public async Task<ActionResult<bool>> ModifyUserStatusAsync([FromBody] ModifyDutyStatusCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.ModifyDutyStatusPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        #endregion

        #region Queries


        #endregion
    }
}
