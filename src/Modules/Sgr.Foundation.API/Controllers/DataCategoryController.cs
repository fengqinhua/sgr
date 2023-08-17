/**************************************************************
 * 
 * 唯一标识：16dade15-c01e-4cc5-b446-2adee86e206e
 * 命名空间：Sgr.Foundation.API.Controllers
 * 创建时间：2023/8/17 15:29:01
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
using Sgr.Foundation.API.Application.Queries.AuditLog;
using Sgr.Foundation.API.Application.Queries.AuditLog.Impl;
using Sgr.Foundation.API.Application.Queries.DataCategory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sgr.Foundation.API.Controllers
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
        public async Task<ActionResult<PagedResponse<OutDataDictionaryItem>>> GetCategoryItemsAsync(InCategpryItemRequest request)
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuditLogActionFilter("查询数据字典项详情")]
        public async Task<ActionResult<OutLogOperate>> GetAsync(long id)
        {
            //权限认证

            if (id < 0)
                return NotFound();

            var result = await _dataCategoryQueries.GetItemByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
