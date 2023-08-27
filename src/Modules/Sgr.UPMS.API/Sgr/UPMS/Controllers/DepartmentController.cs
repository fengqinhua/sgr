/**************************************************************
 * 
 * 唯一标识：be28a09b-52d3-4202-9f45-c4fc25eb41ab
 * 命名空间：Sgr.UPMS.Controllers
 * 创建时间：2023/8/27 20:32:39
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
using Sgr.UPMS.Application.Commands.Departments;
using System.Threading.Tasks;

namespace Sgr.UPMS.Controllers
{
    /// <summary>
    /// 部门管理
    /// </summary>
    [Route("api/v1/sgr/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthorizationService _authorizationService;

        public DepartmentController(IMediator mediator,
            IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _authorizationService = authorizationService;
        }

        #region Command

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("创建部门")]
        public async Task<ActionResult<bool>> CreateAsync([FromBody] CreateDepartmentCommand command)
        {
            if (await _authorizationService.AuthorizeAsync(User, Permissions.CreateDepartmentPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("修改部门")]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] UpdateDepartmentCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.UpdateDepartmentPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("删除部门")]
        public async Task<ActionResult<bool>> DeleteAsync([FromBody] DeleteDepartmentCommand command)
        {
            //权限认证
            if (await _authorizationService.AuthorizeAsync(this.User, Permissions.DeleteDepartmentPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        #endregion

        #region Queries


        #endregion
    }
}
