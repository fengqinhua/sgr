/**************************************************************
 * 
 * 唯一标识：2638f30f-5278-4205-b507-680a1c93f567
 * 命名空间：Sgr.UPMS.Application.Validations
 * 创建时间：2023/8/24 19:02:51
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Sgr.UPMS.Application.Commands.Organizations;

namespace Sgr.UPMS.Application.Validations
{
    public class CancellationOrgCommandValidator : AbstractValidator<CancellationOrgCommand>
    {
        public CancellationOrgCommandValidator()
        {
            RuleFor(command => command.Password).NotEmpty();
        }
    }
}
