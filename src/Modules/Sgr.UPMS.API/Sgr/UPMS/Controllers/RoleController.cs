/**************************************************************
 * 
 * 唯一标识：cba256ae-0755-4fa8-8cdb-338009b0a8e7
 * 命名空间：Sgr.UPMS.Controllers
 * 创建时间：2023/8/27 19:05:27
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
using Sgr.UPMS.Application.Commands.Roles;
using Sgr.UPMS.Application.Commands.Users;
using System.Threading.Tasks;

namespace Sgr.UPMS.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Route("api/v1/sgr/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthorizationService _authorizationService;

        public RoleController(IMediator mediator,
            IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _authorizationService = authorizationService;
        }

        #region Command

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("创建角色")]
        public async Task<ActionResult<bool>> CreateAsync([FromBody] CreateRoleCommand command)
        {
            if (await _authorizationService.AuthorizeAsync(User, Permissions.CreateRolePermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("修改角色")]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] UpdateRoleCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.UpdateRolePermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("删除角色")]
        public async Task<ActionResult<bool>> DeleteAsync([FromBody] DeleteRoleCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.DeleteRolePermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 调整角色状态
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("change-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("调整角色状态")]
        public async Task<ActionResult<bool>> ModifyUserStatusAsync([FromBody] ModifyRoleStatusCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.ModifyRoleStatusPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 授予功能权限
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("allocate-functionpermission")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("授予功能权限")]
        public async Task<ActionResult<bool>> ResetPasswordAsync([FromBody] AllocateFunctionPermissionCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.AllocateFunctionPermissionPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        #endregion

        #region Queries


        #endregion
    }
}
