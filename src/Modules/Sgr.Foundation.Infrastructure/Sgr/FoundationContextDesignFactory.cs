/**************************************************************
 * 
 * 唯一标识：8a67ccc0-4c49-4589-8128-d564d6451447
 * 命名空间：Sgr
 * 创建时间：2023/8/4 11:10:47
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr
{
    /// <summary>
    /// 
    /// </summary>
    public class FoundationContextDesignFactory : IDesignTimeDbContextFactory<FoundationContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public FoundationContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
