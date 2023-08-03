/**************************************************************
 * 
 * 唯一标识：91eced7b-2995-4820-8b2d-4f71be3107ab
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/8/3 11:11:50
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.Domain.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessRuleValidationException: BusinessException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkResult"></param>
        public BusinessRuleValidationException(BusinessCheckResult checkResult)
            : base(checkResult.Message)
        {

        }
    }
}
