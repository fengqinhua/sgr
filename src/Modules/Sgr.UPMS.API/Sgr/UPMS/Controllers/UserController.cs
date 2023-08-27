/**************************************************************
 * 
 * 唯一标识：1dad0b24-9f2f-406a-bedf-0ba5e8a24642
 * 命名空间：Sgr.UPMS.Controllers
 * 创建时间：2023/8/27 17:03:35
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
using Sgr.UPMS.Application.Commands.Organizations;
using Sgr.UPMS.Application.Commands.Roles;
using Sgr.UPMS.Application.Commands.Users;
using System.Threading.Tasks;

namespace Sgr.UPMS.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/v1/sgr/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthorizationService _authorizationService;

        public UserController(IMediator mediator,
            IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _authorizationService = authorizationService;
        }


        #region Command

        /// <summary>
        /// 创建账号
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("创建账号")]
        public async Task<ActionResult<bool>> CreateAsync([FromBody] CreateUserCommand command)
        {
            if (await _authorizationService.AuthorizeAsync(User, Permissions.CreateUserPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 修改账号
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("修改账号")]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] UpdateUserCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.UpdateUserPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 删除账号
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("删除账号")]
        public async Task<ActionResult<bool>> DeleteAsync([FromBody] DeleteUserCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.DeleteUserPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 调整账号状态
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("change-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("调整账号状态")]
        public async Task<ActionResult<bool>> ModifyUserStatusAsync([FromBody] ModifyUserStatusCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.ModifyUserStatusPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("重置密码")]
        public async Task<ActionResult<bool>> ResetPasswordAsync([FromBody] ResetPasswordCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.ResetPasswordPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }


        /// <summary>
        /// 用户中心-修改个人信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("change-personal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("用户中心-修改个人信息")]
        public async Task<ActionResult<bool>> ModifyUserAsync([FromBody] ModifyUserCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.ModifyUserPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 用户中心-修改密码
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("用户中心-修改密码")]
        public async Task<ActionResult<bool>> ModifyPasswordAsync([FromBody] ModifyPasswordCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.ModifyPasswordPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }


        #endregion


        #region Queries


        #endregion
    }
}
