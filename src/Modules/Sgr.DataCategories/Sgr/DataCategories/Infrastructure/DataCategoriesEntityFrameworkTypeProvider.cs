/**************************************************************
 * 
 * 唯一标识：0d13cb3b-c45c-4e6b-b54b-ae0e1c7ef83b
 * 命名空间：Sgr.DataCategories.Infrastructure
 * 创建时间：2023/8/20 16:48:36
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using Sgr.DataCategories.Domain;
using Sgr.DataCategories.Infrastructure.EntityConfigurations;
using Sgr.EntityFrameworkCore;

namespace Sgr.DataCategories.Infrastructure
{
    public class DataCategoriesEntityFrameworkTypeProvider : IEntityFrameworkTypeProvider
    {
        public void RegisterEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataCategoryItem>();
        }

        public void RegisterEntityConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DataCategoryItemConfiguration());
        }
    }
}
