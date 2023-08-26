/**************************************************************
 * 
 * 唯一标识：99b060da-1e92-40c8-a149-eda1679394bf
 * 命名空间：Sgr.UPMS.Controllers
 * 创建时间：2023/8/25 15:43:42
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sgr.AspNetCore.ActionFilters;
using Sgr.AspNetCore.ActionFilters.AuditLogs;
using Sgr.ExceptionHandling;
using Sgr.Identity.Services;
using Sgr.Oss.Services;
using Sgr.UPMS.Application.Commands.Organizations;
using Sgr.UPMS.Application.Queries;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.UPMS.Controllers
{
    /// <summary>
    /// 组织机构
    /// </summary>
    [Route("api/v1/sgr/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPermissionChecker _permissionChecker;
        private readonly IOrganizationQueries _organizationQueries;

        public OrganizationController(IMediator mediator,
            IOrganizationQueries organizationQueries,
            IPermissionChecker permissionChecker)
        {
            _mediator = mediator;
            _organizationQueries = organizationQueries;
            _permissionChecker = permissionChecker;
        }

        #region Commond


        /// <summary>
        /// 创建组织机构
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("创建组织机构")]
        public async Task<ActionResult<bool>> CreateAsync([FromBody] CreateOrgCommand command)
        {
            //权限认证
            if (await _permissionChecker.IsGrantedAsync(this.User, Permissions.CreateOrgPermission))
                return this.CustomUnauthorized();
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 修改组织机构
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("修改组织机构")]
        public async Task<ActionResult<bool>> UpdateOrgAsync([FromBody] UpdateOrgCommand command)
        {
            //权限认证
            if (await _permissionChecker.IsGrantedAsync(this.User, Permissions.UpdateOrgPermission))
                return this.CustomUnauthorized();
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 注销组织机构
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("注销组织机构")]
        public async Task<ActionResult<bool>> CancellationOrgAsync([FromBody] CancellationOrgCommand command)
        {
            //权限认证
            if (await _permissionChecker.IsGrantedAsync(this.User, Permissions.CancellationOrgPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 注册组织机构
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("注册组织机构")]
        public async Task<ActionResult<bool>> RegisterOrgAsync([FromBody] RegisterOrgCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 组织机构认证
        /// </summary>
        /// <param name="command"></param>
        /// <param name="formCollection"></param>
        /// <param name="ossService"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("authentication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("组织机构认证")]
        public async Task<ActionResult<bool>> AuthenticationAsync([FromBody] AuthenticationCommand command,
            [FromForm] IFormCollection formCollection,
            [FromServices] IOssService ossService)
        {
            //权限认证
            if (await _permissionChecker.IsGrantedAsync(this.User, Permissions.AuthenticationOrgPermission))
                return this.CustomUnauthorized();

            if (command.Id <= 0)
                return this.CustomBadRequest($"Id({command.Id})需大于零!");

            if (formCollection == null || formCollection.Files.Count < 0)
                return this.CustomBadRequest($"未上传营业执照!");

            //附件保存
            IFormFile formFile = formCollection.Files[0];
            var suffix = Path.GetExtension(formFile.FileName).ToLower();

            string objectName = $"{command.Id}_org_businesslicense.{suffix}";
            command.BusinessLicenseObjectName = objectName;
            if (!await _mediator.Send(command))
                return this.CustomBadRequest($"组织机构认证信息提交失败!");


            if (await ossService.PutImageAsync(objectName, formFile.OpenReadStream()))
                return this.CustomBadRequest($"营业执照上传失败!");

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 绑定组织机构
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("associated_parent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("绑定组织机构")]
        public async Task<ActionResult<bool>> AssociatedParentOrgAsync([FromBody] AssociatedParentOrgCommand command)
        {
            //权限认证
            if (await _permissionChecker.IsGrantedAsync(this.User, Permissions.AssociatedParentOrgPermission))
                return this.CustomUnauthorized();

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 上传Logo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formCollection"></param>
        /// <param name="ossService"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("logo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status401Unauthorized)]
        [AuditLogActionFilter("上传Logo")]
        public async Task<ActionResult<bool>> ModifyOrgLogoAsync([FromQuery] long id, 
            [FromForm] IFormCollection formCollection,
            [FromServices] IOssService ossService)
        {
            //权限认证
            if (await _permissionChecker.IsGrantedAsync(this.User, Permissions.ModifyOrgLogoPermission))
                return this.CustomUnauthorized();

            if (id <= 0)
                return this.CustomBadRequest($"Id({id})需大于零!");

            if(formCollection == null || formCollection.Files.Count < 0)
                return this.CustomBadRequest($"未上传 Logo!");

            //附件保存
            IFormFile formFile = formCollection.Files[0];
            var suffix = Path.GetExtension(formFile.FileName).ToLower();
            string objectName = $"{id}_org_logo.{suffix}";

            ModifyOrgLogoCommand command = new ModifyOrgLogoCommand()
            {
                Id = id,
                LogoObjectName = objectName
            };

            if(!await _mediator.Send(command) || !await ossService.PutImageAsync(objectName, formFile.OpenReadStream()))
                return this.CustomBadRequest($"Logo上传失败!");


            return Ok(true);
        }

        #endregion

        #region Queries



        #endregion 

    }
}
