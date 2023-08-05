/**************************************************************
 * 
 * 唯一标识：eaf96086-a21e-4dfc-9398-b25124fff6ee
 * 命名空间：Sgr.EntityFrameworkCore
 * 创建时间：2023/8/4 16:38:41
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityFrameworkCore
{
    /// <summary>
    /// EntityFramework 相关Entity及Configuration类型提供者
    /// </summary>
    public interface IEntityFrameworkTypeProvider
    {
        /// <summary>
        /// 注册Entity
        /// </summary>
        /// <param name="modelBuilder"></param>
        void RegisterEntities(ModelBuilder modelBuilder);

        /// <summary>
        /// 注册Configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        void RegisterEntityConfigurations(ModelBuilder modelBuilder);
    }
}
