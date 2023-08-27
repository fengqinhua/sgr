/**************************************************************
 * 
 * 唯一标识：4ddb4740-4e1f-4872-a989-3ae90319b3c5
 * 命名空间：Sgr.UPMS.Application.Validations.Duties
 * 创建时间：2023/8/27 19:26:43
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Sgr.UPMS.Application.Commands.Duties;

namespace Sgr.UPMS.Application.Validations.Duties
{
    public class CreateDutyCommandValidator : AbstractValidator<CreateDutyCommand>
    {
        public CreateDutyCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MaximumLength(200);
            RuleFor(command => command.Remarks).MaximumLength(500);
        }
    }
}
