/**************************************************************
 * 
 * 唯一标识：b25f5c0d-8d9f-4486-82a4-5bca46b447e1
 * 命名空间：Sgr.Domain.Entities
 * 创建时间：2023/8/3 10:11:58
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
    /// 业务规则检查返回结果
    /// </summary>
    public sealed class BusinessCheckResult
    {
        private BusinessCheckResult(bool isComply)
        {
            IsComply = isComply;
        }

        private BusinessCheckResult(bool isComply, string message)
        {
            IsComply = isComply;
            Message = message;
        }

        /// <summary>
        /// 是否符合业务规则
        /// </summary>
        public bool IsComply { get; private set; }

        /// <summary>
        /// 业务规则校验消息
        /// </summary>
        public string Message { get; private set; } = string.Empty;

        /// <summary>
        /// 符合业务规则
        /// </summary>
        /// <returns></returns>
        public static BusinessCheckResult Ok()
        {
            return new BusinessCheckResult(true);
        }

        /// <summary>
        /// 不符合业务规则
        /// </summary>
        /// <param name="mgs"></param>
        /// <returns></returns>
        public static BusinessCheckResult Fail(string mgs)
        {
            return new BusinessCheckResult(false, mgs);
        }

    }
}
