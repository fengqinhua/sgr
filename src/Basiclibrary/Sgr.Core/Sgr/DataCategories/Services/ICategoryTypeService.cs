using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.DataCategories.Services
{
    public interface ICategoryTypeService
    {
        /// <summary>
        /// 根据值获取数据字典分类信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<NameValue?> GetAsync(string value);
        /// <summary>
        /// 获取所有数据字典分类信息
        /// </summary>
        /// <returns></returns>
        Task<NameValue[]> GetAllAsync();
    }
}
