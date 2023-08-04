/**************************************************************
 * 
 * 唯一标识：f89ac573-81ab-4664-bdb3-4721ed589eb3
 * 命名空间：Sgr.EntityFrameworkCore
 * 创建时间：2023/8/4 10:46:31
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Sgr.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgr.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public class LongValueGenerator : ValueGenerator<long>
    {
        /// <summary>
        /// 
        /// </summary>
        public override bool GeneratesTemporaryValues => false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override long Next(EntityEntry entry)
        {
            Check.NotNull(entry, nameof(entry));

            //var gen = entry.Context.GetService<INumberIdGenerator>();
            //return gen.GenerateUniqueId();

            return entry.CurrentValues.GetValue<long>("RowVersion") + 1;

    
        }
    }
}
