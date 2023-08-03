/**************************************************************
 * 
 * 唯一标识：445f73bc-0387-49b0-afa9-eac0f73d8af7
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/8/3 11:14:02
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public static class BusinessCheckResultExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkResult"></param>
        /// <exception cref="BusinessRuleValidationException"></exception>
        public static void HandleRule(this BusinessCheckResult checkResult)
        {
            Check.NotNull(checkResult, nameof(checkResult));

            if (!checkResult.IsComply)
                throw new BusinessRuleValidationException(checkResult);
        }
    }
}
