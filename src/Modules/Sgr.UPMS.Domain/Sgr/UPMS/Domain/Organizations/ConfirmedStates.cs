/**************************************************************
 * 
 * 唯一标识：9d980f8c-b2f1-4459-8dd6-80124bc5e19e
 * 命名空间：Sgr.UPMS.Domain.Organizations
 * 创建时间：2023/8/23 15:20:11
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Sgr.UPMS.Domain.Organizations
{
    public enum ConfirmedStates
    {
        /// <summary>
        /// 待认证
        /// </summary>
        WaitingConfirmed = 0,
        /// <summary>
        /// 待审核
        /// </summary>
        WaitingReview,
        /// <summary>
        /// 已认证
        /// </summary>
        ConfirmedCompleted
    }
}
