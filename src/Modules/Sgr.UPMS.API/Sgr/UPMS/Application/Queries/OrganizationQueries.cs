/**************************************************************
 * 
 * 唯一标识：ebb3a326-6e70-4ae0-b812-30aca37b1ff1
 * 命名空间：Sgr.UPMS.Application.Queries
 * 创建时间：2023/8/25 20:25:28
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.DataCategories.Services;
using Sgr.EntityFrameworkCore;

namespace Sgr.UPMS.Application.Queries
{
    public class OrganizationQueries : IOrganizationQueries
    {
        private readonly SgrDbContext _context;
        private readonly ICategoryTypeService _categoryTypeService;

        public OrganizationQueries(SgrDbContext context, ICategoryTypeService categoryTypeService)
        {
            _context = context;
            _categoryTypeService = categoryTypeService;
        }



    }
}
