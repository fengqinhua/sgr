/**************************************************************
 * 
 * 唯一标识：b5003a4f-3c04-4e0f-b217-aacf07bb9cb7
 * 命名空间：MediatR.Behaviors
 * 创建时间：2023/8/18 9:26:46
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Domain.Entities;
using Sgr.Exceptions;
using System;

namespace MediatR.Behaviors
{

    public class MediatRValidationException : BusinessException
    {
        public MediatRValidationException(string message)
            : base(message)
        { }

        public MediatRValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
