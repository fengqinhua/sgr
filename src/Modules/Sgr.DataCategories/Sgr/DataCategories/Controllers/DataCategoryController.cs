/**************************************************************
 * 
 * 唯一标识：9b5ffe62-1b15-42ce-9f36-539511816351
 * 命名空间：Sgr.DataCategories.Controllers
 * 创建时间：2023/8/20 16:57:30
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sgr.Application.ViewModels;
using Sgr.AspNetCore.ActionFilters;
using Sgr.AspNetCore.ActionFilters.AuditLogs;
using Sgr.DataCategories.Application.Commands;
using Sgr.DataCategories.Application.Queries;
using Sgr.DataCategories.Application.ViewModels;
using Sgr.ExceptionHandling;
using System.Threading.Tasks;


namespace Sgr.DataCategories.Controllers
{
    /// <summary>
    /// 数据字典
    /// </summary>
    //[Authorize]
    [Route("api/v1/sgr/[controller]")]
    [ApiController]
    public class DataCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDataCategoryQueries _dataCategoryQueries;

        public DataCategoryController(IMediator mediator, IDataCategoryQueries dataCategoryQueries)
        {
            _mediator = mediator;
            _dataCategoryQueries = dataCategoryQueries;
        }

        #region Commond

        /// <summary>
        /// 创建数据字典项
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AuditLogActionFilter("创建数据字典项")]
        public async Task<ActionResult<bool>> CreateAsync([FromBody] CreateCategpryItemCommand command)
        {
            //权限认证
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        /// <summary>
        /// 创建数据字典项
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AuditLogActionFilter("修改数据字典项")]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] UpdateCategpryItemCommand command)
        {
            //权限认证
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        /// <summary>
        /// 删除数据字典项
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AuditLogActionFilter("删除数据字典项")]
        public async Task<ActionResult<bool>> DeleteAsync([FromBody] DeleteCategpryItemCommand command)
        {
            //权限认证
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        #endregion


        #region Queries

        /// <summary>
        /// 查询字典类型列表
        /// </summary>
        /// <returns></returns>
        [Route("types")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AuditLogActionFilter("查询字典类型列表")]
        public async Task<ActionResult<NameValue[]>> GetCategoryTypesAsync()
        {
            //权限认证
            var result = await _dataCategoryQueries.GetCategoryTypesAsync();
            return Ok(result);
        }

        /// <summary>
        /// 查询符合条件的数据字典项集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("list")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AuditLogActionFilter("查询符合条件的数据字典项集合")]
        public async Task<ActionResult<PagedResponse<DataDictionaryItemModel>>> GetCategoryItemsAsync(CategpryItemSearchModel request)
        {
            //权限认证
            var result = await _dataCategoryQueries.GetItemsByCategoryTypeAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// 查询数据字典项详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        [Route("items/{id:long}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status404NotFound)]
        [AuditLogActionFilter("查询数据字典项详情")]
        public async Task<ActionResult<DataDictionaryItemModel>> GetAsync(long id)
        {
            //权限认证

            if (id <= 0)
                return this.CustomBadRequest($"Id({id})需大于零");

            var result = await _dataCategoryQueries.GetItemByIdAsync(id);

            if (result == null)
                return this.CustomNotFound($"查无数据,Id={id}");

            return Ok(result);
        }


        #endregion


    }
}